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

                _context.Utenti.Add(registrationEntity);
                _context.SaveChanges();

                Utente utenteDb = _context.Utenti.Where(x=>x.CodiceFiscale.Equals(registrationEntity.CodiceFiscale)).FirstOrDefault();
                if (utenteDb != null)
                {
                    var utentiRuoli = new UtenteRuolo
                    {
                        IdUtente = utenteDb.IdUtente,
                        IdRuolo = 2,
                    };
                    _context.UtentiRuoli.Add(utentiRuoli);
                    _context.SaveChanges();
                }

                return RedirectToAction("RegisterSuccess", "Login",registrationEntity);
            }

            return View(registerModel);
        }
        private CheckResult CheckUtenteEsiste(RegistrationViewModel model)
        {
            var result = new CheckResult
            {
                IsUsernameTaken = _context.Utenti.Any(r => r.Username == model.Username),
                IsCodiceFiscaleTaken = _context.Utenti.Any(r => r.CodiceFiscale == model.CodiceFiscale),
                IsEmailTaken = _context.Utenti.Any(r => r.Email == model.Email)
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
            Utente utente = _context.Utenti
                           .FirstOrDefault(x => x.Username.ToLower().Trim() == model.Username.ToLower().Trim() &&
                                                x.Password.ToLower().Trim() == model.Password.ToLower().Trim());
            if (utente != null)
            {
                var utenteJson = JsonConvert.SerializeObject(utente);
                HttpContext.Session.SetString("Utente", utenteJson);

                var utenteRuolo = _context.UtentiRuoli
                    .Where(ur => ur.Ruolo.IdRuolo == _context.Ruoli.Min(r => r.IdRuolo)) 
                    .Select(ur => new
                    {
                        utenteId = ur.Utente.IdUtente,
                        ruoloId = ur.Ruolo.IdRuolo
                    })
                    .FirstOrDefault();

                int ruoloId = utenteRuolo.ruoloId;

                Ruolo ruolo = _context.Ruoli
                    .FirstOrDefault(r => r.IdRuolo == ruoloId);

                var ruoloJson = JsonConvert.SerializeObject(ruolo);
                HttpContext.Session.SetString("Ruolo", ruoloJson);
                return true;
            }
            return false;
        }




    }
}
