using Microsoft.AspNetCore.Mvc;
using LoginStartSolution.Models;
using System.Diagnostics;
using LoginStartSolution.Models.LoginModels;
using Newtonsoft.Json;

namespace LoginStartSolution.Controllers
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
            var personaJson = HttpContext.Session.GetString("Persona");
            if (!string.IsNullOrEmpty(personaJson))
            {
                var persona = JsonConvert.DeserializeObject<RegistrationViewModel>(personaJson);
                return View(persona);
            }
            return View();
        }



    }
}
