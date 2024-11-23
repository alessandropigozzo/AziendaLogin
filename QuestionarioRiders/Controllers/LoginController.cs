using Microsoft.AspNetCore.Mvc;
using QuestionarioRiders.Models;
using QuestionarioRiders.Models.LoginModels;
using System.Diagnostics;

namespace QuestionarioRiders.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ApplicationDbContext _context;

        public LoginController(ILogger<LoginController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationViewModel registerModel)
        {
            if(ModelState.IsValid)
            {
                var registrationEntity = new RegistrationViewModel
                {
                    Email = registerModel.Email,
                    Username = registerModel.Username,
                    Password = registerModel.Password,
                    Nome = registerModel.Nome,
                    Cognome = registerModel.Cognome,
                    CodiceFiscale = registerModel.CodiceFiscale
                };
                _context.Registrazione.Add(registrationEntity);
                _context.SaveChanges(); 
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Logica di autenticazione qui (es. controllo dell'utente nel database)

            return RedirectToAction("Index", "Home"); // Reindirizza dopo il login
        }

        [HttpPost]
        public IActionResult Index(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Logica di autenticazione qui (es. controllo dell'utente nel database)

            return RedirectToAction("Register", "Home"); // Reindirizza dopo il login
        }



    }
}
