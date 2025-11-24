using Microsoft.AspNetCore.Mvc;
using UmetnickaDelaProjekat1.Models;

namespace UmetnickaDelaProjekat1.Controllers
{
    public class UmetnickaDelaController : Controller
    {
        private readonly UmetnickaDelaContext _context;

        public UmetnickaDelaController(UmetnickaDelaContext context)
        {
            _context = context;
        }

        // Prikaz svih umetničkih dela
        public IActionResult Index(string Kategorija, string Umetnik, decimal? CenaMin, decimal? CenaMax, bool? Dostupnost)
        {
            var query = _context.UmetnickaDela.AsQueryable();

            if (!string.IsNullOrEmpty(Kategorija))
                query = query.Where(u => u.Kategorija.Contains(Kategorija));

            if (!string.IsNullOrEmpty(Umetnik))
                query = query.Where(u => u.Umetnik.Contains(Umetnik));

            if (CenaMin.HasValue)
                query = query.Where(u => u.Cena >= CenaMin.Value);

            if (CenaMax.HasValue)
                query = query.Where(u => u.Cena <= CenaMax.Value);

            if (Dostupnost.HasValue)
                query = query.Where(u => u.Dostupnost == Dostupnost.Value);

            return View(query.ToList());
        }

        // Dodavanje novog dela
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UmetnickaDela delo)
        {
            if (ModelState.IsValid)
            {
                _context.UmetnickaDela.Add(delo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(delo);
        }

        // Ažuriranje dela
        public IActionResult Edit(int id)
        {
            var delo = _context.UmetnickaDela.Find(id);
            if (delo == null)
            {
                return NotFound();
            }
            return View(delo);
        }

        [HttpPost]
        public IActionResult Edit(UmetnickaDela delo)
        {
            if (ModelState.IsValid)
            {
                _context.UmetnickaDela.Update(delo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(delo);
        }

        // Brisanje dela

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delo = _context.UmetnickaDela
                .FirstOrDefault(m => m.Id == id);
            if (delo == null)
            {
                return NotFound();
            }

            return View(delo);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var delo = _context.UmetnickaDela.Find(id);
            if (delo == null)
            {
                return NotFound();
            }

            
            var stavke = _context.StavkePorudzbine
                                 .Where(s => s.UmetnickoDeloId == id)
                                 .ToList();
            _context.StavkePorudzbine.RemoveRange(stavke);

           
            _context.UmetnickaDela.Remove(delo);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


    }
}