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
                return View(accesso);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                var accessoJson = HttpContext.Session.GetString("AccessoUtente");
                var accesso = JsonConvert.DeserializeObject<AccessoUtente>(accessoJson);
                var idImmagine = accesso.utente.IdImmagine; 

                byte[] imageData;

                // Leggi l'immagine come array di byte
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var binaryReader = new BinaryReader(fileStream))
                    {
                        imageData = binaryReader.ReadBytes((int)fileStream.Length);
                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Immagini SET img1 = @ImageData WHERE IdImmagine = @IdImmagine";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ImageData", imageData);
                        command.Parameters.AddWithValue("@IdImmagine", idImmagine);
                        command.ExecuteNonQuery();
                    }
                }
            }
            return RedirectToAction("UploadImage");
        }

    }
}
