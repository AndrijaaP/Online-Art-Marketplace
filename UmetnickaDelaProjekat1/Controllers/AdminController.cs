using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UmetnickaDelaProjekat1.Models;

namespace UmetnickaDelaProjekat1.Controllers
{
    public class AdminController : Controller
    {
        private readonly UmetnickaDelaContext _context;

        public AdminController(UmetnickaDelaContext context)
        {
            _context = context;
        }

        // -----------------------------
        // KORISNICI
        // -----------------------------
        public IActionResult Korisnici()
        {
            var korisnici = _context.Korisnici.ToList();
            return View(korisnici);
        }

        public IActionResult ObrisiKorisnika(int id)
        {
            var korisnik = _context.Korisnici.Find(id);
            if (korisnik != null)
            {
                _context.Korisnici.Remove(korisnik);
                _context.SaveChanges();
            }
            return RedirectToAction("Korisnici");
        }

        // -----------------------------
        // UMETNIČKA DELA
        // -----------------------------
        public IActionResult Dela()
        {
            var dela = _context.UmetnickaDela.ToList();
            return View(dela);
        }

        public IActionResult ObrisiDelo(int id)
        {
            var delo = _context.UmetnickaDela.Find(id);
            if (delo != null)
            {
                _context.UmetnickaDela.Remove(delo);
                _context.SaveChanges();
            }
            return RedirectToAction("Dela");
        }
        
    // -----------------------------
// DODAVANJE I IZMENA KORISNIKA
// -----------------------------

public IActionResult DodajKorisnika()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DodajKorisnika(Korisnici korisnik)
        {
            if (ModelState.IsValid)
            {
                _context.Korisnici.Add(korisnik);
                _context.SaveChanges();
                return RedirectToAction("Korisnici");
            }
            return View(korisnik);
        }

        public IActionResult IzmeniKorisnika(int id)
        {
            var korisnik = _context.Korisnici.Find(id);
            if (korisnik == null) return NotFound();

            return View(korisnik);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IzmeniKorisnika(Korisnici korisnik)
        {
            if (ModelState.IsValid)
            {
                _context.Korisnici.Update(korisnik);
                _context.SaveChanges();
                return RedirectToAction("Korisnici");
            }
            return View(korisnik);
        }

        // -----------------------------
        // DODAVANJE I IZMENA DELA
        // -----------------------------

        public IActionResult DodajDelo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DodajDelo(UmetnickaDela delo)
        {
            if (ModelState.IsValid)
            {
                _context.UmetnickaDela.Add(delo);
                _context.SaveChanges();
                return RedirectToAction("Dela");
            }
            return View(delo);
        }

        public IActionResult IzmeniDelo(int id)
        {
            var delo = _context.UmetnickaDela.Find(id);
            if (delo == null) return NotFound();

            return View(delo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IzmeniDelo(UmetnickaDela delo)
        {
            if (ModelState.IsValid)
            {
                _context.UmetnickaDela.Update(delo);
                _context.SaveChanges();
                return RedirectToAction("Dela");
            }
            return View(delo);
        }

    }
}


