using Microsoft.AspNetCore.Mvc;
using LoginStartMenu.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using LoginStartMenu.Models.Collection;
using LoginStartMenu.Models.Entity;

namespace LoginStartMenu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        [HttpGet]
        public IActionResult Index()
        {
            var utenteJson = HttpContext.Session.GetString("Utente");
            var ruoloJson = HttpContext.Session.GetString("Ruolo");
            var immagineJson = HttpContext.Session.GetString("Immagine");

            if (!string.IsNullOrEmpty(utenteJson) && !string.IsNullOrEmpty(ruoloJson))
            {
                var utente = JsonConvert.DeserializeObject<Utente>(utenteJson);
                var ruolo = JsonConvert.DeserializeObject<Ruolo>(ruoloJson);
                var immagine = JsonConvert.DeserializeObject<Immagine>(immagineJson);

                AccessoUtente accesso = new AccessoUtente
                {
                    utente = utente,
                    ruolo = ruolo,
                    immagine = immagine
                };

                // Salvo l'oggetto accesso nella sessione
                HttpContext.Session.SetString("AccessoUtente", JsonConvert.SerializeObject(accesso));

                return View(accesso);
            }

            return View();
        }


    }
}
