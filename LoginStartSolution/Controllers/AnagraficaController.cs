using Microsoft.AspNetCore.Mvc;
using LoginStartMenu.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using LoginStartMenu.Models.Collection;
using Microsoft.Data.SqlClient;
using System;
using System.Data.SqlClient;
using System.IO;
using LoginStartMenu.Models.Collection;


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
                // Salvataggio del file fisico nella cartella 'wwwroot/img'
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Leggere il file come array di byte
                byte[] imageData;
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var binaryReader = new BinaryReader(fileStream))
                    {
                        imageData = binaryReader.ReadBytes((int)fileStream.Length);
                    }
                }

                // Recupero della connessione al database
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                // Ottenere l'accesso dell'utente dalla sessione
                var accessoJson = HttpContext.Session.GetString("AccessoUtente");
                var accesso = JsonConvert.DeserializeObject<AccessoUtente>(accessoJson);
                var idImmagine = accesso.utente.IdImmagine;

                // Costruire la query dinamica in base al campo dell'immagine selezionato
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
                    // Esegui la query di aggiornamento nel database
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ImageData", imageData);
                            command.Parameters.AddWithValue("@IdImmagine", idImmagine);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            return RedirectToAction("UploadImage");
        }

    }
}
