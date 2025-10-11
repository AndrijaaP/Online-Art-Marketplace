using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UmetnickaDelaProjekat1.Models;

namespace UmetnickaDelaProjekat1.Controllers
{
    public class PorudzbineController : Controller
    {
        private readonly UmetnickaDelaContext _context;

        public PorudzbineController(UmetnickaDelaContext context)
        {
            _context = context;
        }

        // Prikaz svih porudžbina
        public IActionResult Index()
        {
            var porudzbine = _context.Porudzbine 
                .Include(p => p.Korisnik)
                .Include(p => p.StavkePorudzbines)
                .ThenInclude(s => s.UmetnickoDelo)
                .ToList();

            return View(porudzbine);
        }

        
        public IActionResult Izmeni(int id)
        {
            var porudzbina = _context.Porudzbine.Find(id);
            if (porudzbina == null)
                return NotFound();

            return View(porudzbina);
        }

        
        [HttpPost]
        public IActionResult Izmeni(Porudzbine model)
        {
            var porudzbina = _context.Porudzbine.Find(model.Id);
            if (porudzbina == null)
                return NotFound();

            porudzbina.Status = model.Status;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Obrisi(int id)
        {
            var porudzbina = _context.Porudzbine
                .Include(p => p.StavkePorudzbines)
                .FirstOrDefault(p => p.Id == id);

            if (porudzbina == null)
                return NotFound();

            
            _context.StavkePorudzbine.RemoveRange(porudzbina.StavkePorudzbines);

            
            _context.Porudzbine.Remove(porudzbina);

            _context.SaveChanges();

            return RedirectToAction("Index"); 
        }

        public IActionResult Istorija()
        {
            int? korisnikId = HttpContext.Session.GetInt32("KorisnikId");
            string tipKorisnika = HttpContext.Session.GetString("TipKorisnika");

            if (korisnikId == null || string.IsNullOrEmpty(tipKorisnika))
                return RedirectToAction("Prijava", "Auth");

            if (tipKorisnika == "Administrator")
            {
                
                var porudzbinePoKorisnicima = _context.Korisnici
                    .Include(k => k.Porudzbines
                        .Where(p => p.Status == "Zavrseno"))
                            .ThenInclude(p => p.StavkePorudzbines)
                                .ThenInclude(s => s.UmetnickoDelo)
                    .Where(k => k.Porudzbines.Any(p => p.Status == "Zavrseno"))
                    .ToList();

                return View("Istorija", porudzbinePoKorisnicima);
            }
            else
            {
                var porudzbine = _context.Porudzbine
                    .Where(p => p.KorisnikId == korisnikId && p.Status == "Zavrseno")
                    .Include(p => p.StavkePorudzbines)
                        .ThenInclude(s => s.UmetnickoDelo)
                    .OrderByDescending(p => p.DatumPorudzbine)
                    .ToList();

                return View("Istorija", porudzbine);
            }
        }




    }
}
