using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using LoginStartMenu.Models;
using LoginStartMenu.Models.LoginModels;
using LoginStartMenu.Models.Entity;

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

                var AnagraficaEntity = new Anagrafica
                {
                    Nazionalita = "Italiana",
                    Eta = 30,
                    Via = "Via Roma",
                    Indirizzo = "1",
                    Cap = 12345
                };

                var immagineEntity = new Immagine
                {
                    Img1 = null,
                    Img2 = null,
                    Img3 = null,
                };

                var registrationEntity = new Utente
                {
                    Email = registerModel.Email,
                    Username = registerModel.Username,
                    Password = registerModel.Password,
                    Nome = registerModel.Nome,
                    Cognome = registerModel.Cognome,
                    CodiceFiscale = registerModel.CodiceFiscale,
                    Anagrafica = AnagraficaEntity,
                    Immagine = immagineEntity
                };

                _context.Utenti.Add(registrationEntity);
                _context.SaveChanges();

                // Verifica se il ruolo con IdRuolo = 2 esiste
                var ruolo = _context.Ruoli.FirstOrDefault(r => r.IdRuolo == 2);

                if (ruolo == null)
                {
                    // Se il ruolo non esiste, crea un nuovo ruolo
                    ruolo = new Ruolo
                    {
                        IdRuolo = 2,  // Se l'IdRuolo è manuale, impostalo esplicitamente
                        NomeRuolo = "Admin" // Imposta il nome del ruolo
                    };

                    _context.Ruoli.Add(ruolo);
                    _context.SaveChanges();
                }

                // Associa l'utente al ruolo
                Utente utenteDb = _context.Utenti.Where(x => x.CodiceFiscale.Equals(registrationEntity.CodiceFiscale)).FirstOrDefault();
                if (utenteDb != null && ruolo != null)
                {
                    var utentiRuoli = new UtenteRuolo
                    {
                        IdUtente = utenteDb.IdUtente,
                        IdRuolo = ruolo.IdRuolo // Associa il ruolo
                    };

                    _context.UtentiRuoli.Add(utentiRuoli);
                    _context.SaveChanges();
                }

                return RedirectToAction("RegisterSuccess", "Login", registrationEntity);
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
            try
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il login.");
                ModelState.AddModelError(string.Empty, "Si è verificato un errore durante l'elaborazione della richiesta.");
            }
            return View(model);
        }


        private bool CheckPersonaDbSaveSession(LoginViewModel model)
        {
            try
            {
                HttpContext.Session.SetString("Utente", "");
                HttpContext.Session.SetString("Ruolo", "");

                Utente utente = _context.Utenti
                    .FirstOrDefault(x => x.Username.ToLower().Trim() == model.Username.ToLower().Trim() &&
                                         x.Password.ToLower().Trim() == model.Password.ToLower().Trim());
                if (utente != null)
                {
                    var utenteJson = JsonConvert.SerializeObject(utente);
                    HttpContext.Session.SetString("Utente", utenteJson);

                    var ruoloMinimoId = _context.UtentiRuoli
                    .Where(x => x.IdUtente == utente.IdUtente)
                    .Min(x => x.Ruolo.IdRuolo);

                    // Trova l'utente-ruolo corrispondente al ruolo minimo
                    var utenteRuolo = _context.UtentiRuoli
                        .Where(x => x.IdUtente == utente.IdUtente && x.Ruolo.IdRuolo == ruoloMinimoId)
                        .Select(ur => new
                        {
                            utenteId = ur.Utente.IdUtente,
                            ruoloId = ur.Ruolo.IdRuolo
                        })
                        .FirstOrDefault();

                    if (utenteRuolo == null)
                    {
                        throw new Exception("Nessun ruolo associato trovato per l'utente.");
                    }

                    int ruoloId = utenteRuolo.ruoloId;

                    Ruolo ruolo = _context.Ruoli.FirstOrDefault(r => r.IdRuolo == ruoloId);

                    if (ruolo == null)
                    {
                        throw new Exception("Ruolo non trovato nel database.");
                    }

                    var ruoloJson = JsonConvert.SerializeObject(ruolo);
                    HttpContext.Session.SetString("Ruolo", ruoloJson);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la verifica dell'utente nel database.");
                throw;
            }
        }





    }
}
