using Microsoft.AspNetCore.Mvc;
using LoginStartMenu.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using LoginStartMenu.Models.Entity;
using LoginStartMenu.Models.Collection;

namespace LoginStartMenu.Controllers
{
    public class AnagraficaController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public AnagraficaController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

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



    }
}
