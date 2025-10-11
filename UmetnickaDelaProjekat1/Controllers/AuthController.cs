using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using UmetnickaDelaProjekat1.Models;

namespace UmetnickaDelaProjekat1.Controllers
{
        public class AuthController : Controller
        {
            private readonly UmetnickaDelaContext _context;

            public AuthController(UmetnickaDelaContext context)
            {
                _context = context;
            }

            [HttpGet]
            public IActionResult Registracija()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Registracija(RegistracijaViewModel model)
            {
                if (ModelState.IsValid)
                {
                    
                    if (_context.Korisnici.Any(k => k.Email == model.Email))
                    {
                        ModelState.AddModelError("Email", "Email je već registrovan.");
                        return View(model);
                    }

                    var korisnik = new Korisnici
                    {
                        Ime = model.Ime,
                        Prezime = model.Prezime,
                        Email = model.Email,
                        TipKorisnika = model.TipKorisnika,
                        LozinkaHash = model.Lozinka
                    };

                    _context.Korisnici.Add(korisnik);
                    _context.SaveChanges();

                TempData["Success"] = "Registracija uspešna! Prijavite se.";
                return RedirectToAction("Prijava");

            }
                
            return View(model);


            }
        [HttpGet]
        public IActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prijava(Prijava model)
        {
            if (ModelState.IsValid)
            {
                var korisnik = _context.Korisnici
                    .FirstOrDefault(k => k.Email == model.Email && k.LozinkaHash == model.Lozinka); 

                if (korisnik == null)
                {
                    ModelState.AddModelError(string.Empty, "Pogrešan email ili lozinka.");
                    return View(model);
                }

                HttpContext.Session.SetInt32("KorisnikId", korisnik.Id);
                HttpContext.Session.SetString("TipKorisnika", korisnik.TipKorisnika); 

                TempData["Success"] = "Uspešno ste se prijavili.";

                //  Redirekcija po ulozi
                if (korisnik.TipKorisnika == "Administrator")
                {
                    return RedirectToAction("Index", "Porudzbine");
                }
                else if (korisnik.TipKorisnika == "Umetnik")
                {
                    return RedirectToAction("Index", "UmetnickaDela"); 
                }
                else if (korisnik.TipKorisnika == "Kupac")
                {
                    return RedirectToAction("Katalog", "Art"); 
                }
                else
                {
                    return RedirectToAction("Katalog", "Art"); 
                }
            }

            return View(model);
        }


    }
}

