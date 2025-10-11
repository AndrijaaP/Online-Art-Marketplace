using Microsoft.AspNetCore.Mvc;
using System;
using UmetnickaDelaProjekat1.Models;

namespace UmetnickaDelaProjekat1.Controllers
{
    public class IzvestajController : Controller
    {
        private readonly UmetnickaDelaContext _context;

        public IzvestajController(UmetnickaDelaContext context)
        {
            _context = context;
        }

        // PRODAJA PO KATEGORIJAMA
        public IActionResult PoKategorijama()
        {
            var podaci = _context.StavkePorudzbine
                .Where(s => s.Porudzbina.Status == "Zavrseno")
                .GroupBy(s => s.UmetnickoDelo.Kategorija)
                .Select(g => new ProdajaIzvestaj
                {
                    Naziv = g.Key,
                    UkupnoProdatih = g.Sum(s => s.Kolicina),
                    UkupnaZarada = g.Sum(s => s.Kolicina * s.CenaPoJedinici)
                })
                .ToList();

            ViewBag.NazivKolone = "Kategorija"; 

            return View("Izvestaj", podaci);
        }

        // Izveštaj po umetnicima
        public IActionResult PoUmetnicima()
        {
            var podaci = _context.StavkePorudzbine
                .Where(s => s.Porudzbina.Status == "Zavrseno")
                .GroupBy(s => s.UmetnickoDelo.Umetnik)
                .Select(g => new ProdajaIzvestaj
                {
                    Naziv = g.Key,
                    UkupnoProdatih = g.Sum(s => s.Kolicina),
                    UkupnaZarada = g.Sum(s => s.Kolicina * s.CenaPoJedinici)
                })
                .ToList();

            ViewBag.NazivKolone = "Umetnik"; 

            return View("Izvestaj", podaci);
        }

        // Analiza prihoda po mesecima
        public IActionResult AnalizaPrihoda()
        {
            var prihodiRaw = _context.StavkePorudzbine
                .Where(s => s.Porudzbina.Status == "Zavrseno" && s.Porudzbina.DatumPorudzbine != null)
                .GroupBy(s => new
                {
                    Mesec = s.Porudzbina.DatumPorudzbine.Value.Month,
                    Godina = s.Porudzbina.DatumPorudzbine.Value.Year
                })
                .Select(g => new
                {
                    Mesec = g.Key.Mesec,
                    Godina = g.Key.Godina,
                    UkupnoProdatih = g.Sum(s => s.Kolicina),
                    UkupnaZarada = g.Sum(s => s.Kolicina * s.CenaPoJedinici)
                })
                .ToList(); 

            var prihodi = prihodiRaw
                .Select(x => new ProdajaIzvestaj
                {
                    Naziv = $"{x.Mesec:D2}.{x.Godina}",
                    UkupnoProdatih = x.UkupnoProdatih,
                    UkupnaZarada = x.UkupnaZarada
                })
                .OrderBy(x => x.Naziv)
                .ToList();

            return View("AnalizaPrihoda", prihodi);
        }


    }
}