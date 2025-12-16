using Microsoft.AspNetCore.Mvc;
using LoginStartMenu.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using LoginStartMenu.Models.Collection;
using Microsoft.Data.SqlClient;
using System;
using System.Data.SqlClient;
using System.IO;


namespace LoginStartMenu.Controllers
{
    public class AnagraficaController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AnagraficaController(ILogger<HomeController> logger, ApplicationDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }




        [HttpGet]
        public IActionResult Index()
        {
            var accessoJson = HttpContext.Session.GetString("AccessoUtente");
            if (!string.IsNullOrEmpty(accessoJson))
            {
                var accesso = JsonConvert.DeserializeObject<AccessoUtente>(accessoJson);
                return View(accesso);
            }
            return RedirectToAction("Index", "Login");
        }


        [HttpGet]
        public IActionResult Search()
        {
            var accessoJson = HttpContext.Session.GetString("AccessoUtente");
            if (!string.IsNullOrEmpty(accessoJson))
            {
                var accesso = JsonConvert.DeserializeObject<AccessoUtente>(accessoJson);
                return View(accesso);
            }
            return RedirectToAction("Index", "Login");
        }



        [HttpGet]
        public IActionResult UploadImage()
        {
            var accessoJson = HttpContext.Session.GetString("AccessoUtente");
            if (!string.IsNullOrEmpty(accessoJson))
            {
                var accesso = JsonConvert.DeserializeObject<AccessoUtente>(accessoJson);
                var utenti = _context.Utenti.ToList();
                ViewBag.utentiList = utenti;
               
                return View(accesso);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile file, string user, string imageField)
        {
            if (file != null && file.Length > 0)
            {
                byte[] imageData;
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    imageData = memoryStream.ToArray(); 
                }

                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(user))
                {
                    return BadRequest("Errore: ID utente non valido.");
                }

                string query = "";
                if (imageField == "Img1")
                {
                    query = "UPDATE Immagini SET img1 = @ImageData WHERE IdImmagine = @IdImmagine";
                }
                else if (imageField == "Img2")
                {
                    query = "UPDATE Immagini SET img2 = @ImageData WHERE IdImmagine = @IdImmagine";
                }
                else if (imageField == "Img3")
                {
                    query = "UPDATE Immagini SET img3 = @ImageData WHERE IdImmagine = @IdImmagine";
                }
                else if (imageField == "ImmagineProfilo")
                {
                    query = "UPDATE Immagini SET ImmagineProfilo = @ImageData WHERE IdImmagine = @IdImmagine";
                }

                if (!string.IsNullOrEmpty(query))
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ImageData", imageData);
                            command.Parameters.AddWithValue("@IdImmagine", user); 
                            command.ExecuteNonQuery();
                        }
                    }
                }

                return RedirectToAction("UploadImage");
            }

            return BadRequest("Nessun file selezionato.");
        }


    }
}
