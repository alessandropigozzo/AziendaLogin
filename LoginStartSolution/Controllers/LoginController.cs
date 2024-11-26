using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using LoginStartMenu.Models.Entity;
using LoginStartMenu.Models;
using LoginStartMenu.Models.LoginModels;

namespace LoginStartMenu.Controllers
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

                var registrationEntity = new Utente
                {
                    Email = registerModel.Email,
                    Username = registerModel.Username,
                    Password = registerModel.Password,
                    Nome = registerModel.Nome,
                    Cognome = registerModel.Cognome,
                    CodiceFiscale = registerModel.CodiceFiscale
                };

                _context.Utente.Add(registrationEntity);
                _context.SaveChanges();

                return RedirectToAction("RegisterSuccess", "Login",registrationEntity);
            }

            return View(registerModel);
        }
        private CheckResult CheckUtenteEsiste(RegistrationViewModel model)
        {
            var result = new CheckResult
            {
                IsUsernameTaken = _context.Utente.Any(r => r.Username == model.Username),
                IsCodiceFiscaleTaken = _context.Utente.Any(r => r.CodiceFiscale == model.CodiceFiscale),
                IsEmailTaken = _context.Utente.Any(r => r.Email == model.Email)
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
            if (ModelState.IsValid)
            {
                bool result = CheckPersonaDbSaveSession(model);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username o password non validi.");
                }
            }
            return View(model); 
        }


        private bool CheckPersonaDbSaveSession(LoginViewModel model)
        {
            // Recupera l'utente dal database
            var utente = _context.Utente
                           .FirstOrDefault(x => x.Username.ToLower().Trim() == model.Username.ToLower().Trim() &&
                                                x.Password.ToLower().Trim() == model.Password.ToLower().Trim());

            string[] splitRuoli = utente.RuoliAssegnati?.Split('|') ?? new string[] { }; 

            if (splitRuoli.Length > 0)
            {
                foreach (var item in splitRuoli)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        Console.WriteLine(item.Trim());
                    }
                }
            }
            else
            {
                Console.WriteLine("Nessun ruolo assegnato.");
            }


            if (utente != null)
            {
                var utenteJson = JsonConvert.SerializeObject(utente);
                HttpContext.Session.SetString("Utente", utenteJson);

                return true;
            }

            return false;
        }




    }
}
