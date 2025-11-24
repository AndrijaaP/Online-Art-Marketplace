using Microsoft.AspNetCore.Mvc;
using UmetnickaDelaProjekat1.Models;

namespace UmetnickaDelaProjekat1.Controllers
{
    public class ArtController : Controller
    {
        private readonly UmetnickaDelaContext _context;

        public ArtController(UmetnickaDelaContext context)
        {
            _context = context;
        }

        public IActionResult Katalog()
        {
            var dela = _context.UmetnickaDela
                .Where(d => d.Dostupnost)
                .ToList();
            return View(dela);
        }

        public IActionResult Detalji(int id)
        {
            var delo = _context.UmetnickaDela
                .FirstOrDefault(d => d.Id == id);

            if (delo == null)
                return NotFound();

            return View(delo);
        }
    }
}

