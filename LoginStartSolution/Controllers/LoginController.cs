using Microsoft.AspNetCore.Mvc;
using LoginStartSolution.Models;
using System.Diagnostics;
using LoginStartSolution.Models.LoginModels;

namespace LoginStartSolution.Controllers
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

        [HttpGet]
        public IActionResult RegisterSuccess(RegistrationViewModel registerModel)
        {
            return View(registerModel);
        }


        [HttpPost]
        public IActionResult Register(RegistrationViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var checkResult = CheckUtenteEsiste(registerModel);

                if (checkResult.IsUsernameTaken)
                {
                    ModelState.AddModelError(nameof(registerModel.Username), "Lo username è già in uso.");
                }
                if (checkResult.IsCodiceFiscaleTaken)
                {
                    ModelState.AddModelError(nameof(registerModel.CodiceFiscale), "Il codice fiscale è già in uso.");
                }
                if (checkResult.IsEmailTaken)
                {
                    ModelState.AddModelError(nameof(registerModel.Email), "L'email è già in uso.");
                }

                if (!ModelState.IsValid)
                {
                    return View(registerModel);
                }

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

                return RedirectToAction("RegisterSuccess", "Login",registrationEntity);
            }

            return View(registerModel);
        }
        private CheckResult CheckUtenteEsiste(RegistrationViewModel model)
        {
            var result = new CheckResult
            {
                IsUsernameTaken = _context.Registrazione.Any(r => r.Username == model.Username),
                IsCodiceFiscaleTaken = _context.Registrazione.Any(r => r.CodiceFiscale == model.CodiceFiscale),
                IsEmailTaken = _context.Registrazione.Any(r => r.Email == model.Email)
            };

            return result;
        }

        private class CheckResult
        {
            public bool IsUsernameTaken { get; set; }
            public bool IsCodiceFiscaleTaken { get; set; }
            public bool IsEmailTaken { get; set; }
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



    }
}
