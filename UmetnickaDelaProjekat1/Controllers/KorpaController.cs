using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UmetnickaDelaProjekat1.Models;

namespace UmetnickaDelaProjekat1.Controllers
{
    public class KorpaController : Controller
    {
        private readonly UmetnickaDelaContext _context;

        public KorpaController(UmetnickaDelaContext context)
        {
            _context = context;
        }

        public IActionResult Dodaj(int id)
        {
            int? korisnikId = HttpContext.Session.GetInt32("KorisnikId");
            if (korisnikId == null)
                return RedirectToAction("Prijava", "Auth");

            // Da li već postoji otvorena porudžbina?
            var porudzbina = _context.Porudzbine
                .FirstOrDefault(p => p.KorisnikId == korisnikId && p.Status == "Obrada");

            if (porudzbina == null)
            {
                porudzbina = new Porudzbine
                {
                    KorisnikId = korisnikId.Value,
                    DatumPorudzbine = DateTime.Now,
                    Status = "Obrada"
                };
                _context.Porudzbine.Add(porudzbina);
                _context.SaveChanges();
            }

            
            var delo = _context.UmetnickaDela.Find(id);
            if (delo == null)
                return NotFound();

            var stavka = new StavkePorudzbine
            {
                PorudzbinaId = porudzbina.Id,
                UmetnickoDeloId = delo.Id,
                Kolicina = 1,
                CenaPoJedinici = delo.Cena
            };

            _context.StavkePorudzbine.Add(stavka);
            _context.SaveChanges();

            return RedirectToAction("Katalog", "Art");
        }

        public IActionResult Pregled()
        {
            int? korisnikId = HttpContext.Session.GetInt32("KorisnikId");
            if (korisnikId == null)
                return RedirectToAction("Prijava", "Auth");

            var porudzbina = _context.Porudzbine
                .Where(p => p.KorisnikId == korisnikId && p.Status == "Obrada")
                .FirstOrDefault();

            if (porudzbina == null)
                return View(new List<StavkePorudzbine>());

            var stavke = _context.StavkePorudzbine
            .Where(s => s.PorudzbinaId == porudzbina.Id)
            .Include(s => s.UmetnickoDelo) 
            .ToList();
             return View(stavke);
        }

        public IActionResult Kupi()
        {
            int? korisnikId = HttpContext.Session.GetInt32("KorisnikId");
            if (korisnikId == null)
                return RedirectToAction("Prijava", "Auth");

            var porudzbina = _context.Porudzbine
                .FirstOrDefault(p => p.KorisnikId == korisnikId && p.Status == "Obrada");

            if (porudzbina != null)
            {
                porudzbina.Status = "Zavrseno";
                porudzbina.DatumPorudzbine = DateTime.Now; 
                _context.SaveChanges();
            }

            return RedirectToAction("Katalog", "Art");
        }

    }
}
