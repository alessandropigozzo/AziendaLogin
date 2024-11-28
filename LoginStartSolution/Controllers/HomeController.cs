using Microsoft.AspNetCore.Mvc;
using LoginStartMenu.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using LoginStartMenu.Models.Entity;
using LoginStartMenu.Models.Collection;

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
            if (!string.IsNullOrEmpty(utenteJson)){
                var utente = JsonConvert.DeserializeObject<Utente>(utenteJson);
                var ruolo = JsonConvert.DeserializeObject<Ruolo>(ruoloJson);
                AccessoUtente accesso = new AccessoUtente();
                accesso.utente = utente;
                accesso.ruolo = ruolo;
                return View(accesso);
            }
            return View();
        }



    }
}
