using Microsoft.AspNetCore.Mvc;
using LoginStartMenu.Models;
using System.Diagnostics;
using Newtonsoft.Json;
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
            if (!string.IsNullOrEmpty(utenteJson))
            {
                var utente = JsonConvert.DeserializeObject<Utente>(utenteJson);
                return View(utente);
            }
            return View();
        }



    }
}
