using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektniZadatak.Data;
using ProjektniZadatak.Models;

namespace ProjektniZadatak.Controllers
{
    public class PrijemController : Controller
    {
        private readonly KlinikaContext _context;
        private readonly List<Ljekar> specijalisti;

        public PrijemController(KlinikaContext context)
        {
            _context = context;
            specijalisti = _context.Ljekari.Where(l => l.Titula == Titula.Specijalista).ToList();
        }

        // GET: Prijem
        public async Task<IActionResult> Index()
        {
            var klinikaContext = _context.Prijemi.Include(p => p.Ljekar).Include(p => p.Pacijent);
            return View(await klinikaContext.ToListAsync());
        }

        // GET: Prijem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prijemi == null)
            {
                return NotFound();
            }

            var prijem = await _context.Prijemi
                .Include(p => p.Ljekar)
                .Include(p => p.Pacijent)
                .Include(p => p.Nalaz)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (prijem == null)
            {
                return NotFound();
            }

            return View(prijem);
        }

        // GET: Prijem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prijem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DatumVrijemePrijema,PacijentID,LjekarID,HitniPrijem")] Prijem prijem)
        {
            if (ModelState.IsValid)
            {
                //only doctors with title "specijalista" can be selected 
                if (specijalisti.FirstOrDefault(s => s.ID == prijem.LjekarID) != null)
                {
                    _context.Add(prijem);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } else if (prijem.LjekarID != 0)
                {
                    ViewBag.ErrorSpecijalista = "Možete izabrati samo ljekare sa titulom \"Specijalista\".";
                }
                return View(prijem);
            } 
            return View(prijem);
        }

        // GET: Prijem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prijemi == null)
            {
                return NotFound();
            }

            var prijem = await _context.Prijemi.FindAsync(id);
            if (prijem == null)
            {
                return NotFound();
            }
            var pacijent = _context.Prijemi
                .Include(p => p.Pacijent)
                .FirstOrDefault(p => p.ID == id);
            var ljekar = _context.Prijemi
                .Include(item => item.Ljekar)
                .FirstOrDefault(p => p.ID == id);

            if (pacijent != null)
            {
                if (pacijent != null && pacijent.Pacijent != null)
                {
                    ViewData["PacijentKnjizica"] = pacijent.Pacijent.BrojZdravstveneKnjizice;
                    ViewData["PacijentInfo"] = "Ime i prezime: " + pacijent.Pacijent.Ime + " " + pacijent.Pacijent.Prezime;
                }
            }
            else ViewData["PacijentKnjizica"] = "";
            
            if (ljekar != null)
            {
                if (ljekar != null && ljekar.Ljekar != null)
                {
                    ViewData["LjekarSifra"] = ljekar.Ljekar.Sifra;
                    ViewData["LjekarInfo"] = "Ime i prezime: " + ljekar.Ljekar.Ime + " " + ljekar.Ljekar.Prezime;
                }
            }
            else ViewData["LjekarSifra"] = "";

            return View(prijem);
        }

        // POST: Prijem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, DatumVrijemePrijema, PacijentID, LjekarID, HitniPrijem")] Prijem prijem)
        {
            if (id != prijem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //only doctors with title "specijalista" can be selected 
                if (specijalisti.FirstOrDefault(s => s.ID == prijem.LjekarID) != null)
                {
                    try
                    {
                        _context.Update(prijem);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PrijemExists(prijem.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                } else
                {
                    ViewBag.ErrorLjekarID = "Možete izabrati samo ljekare sa titulom \"Specijalista\".";
                }
                   
            }
            
            if (prijem.PacijentID == 0)
            {
                ViewBag.PacijentKnjizica = "";
                ViewBag.ErrorPacijentID = "Ne postoji pacijent s navedenim brojem zdravstvene knjižice.";   
            } else
            {
                var pacijent = _context.Prijemi.Include(p => p.Pacijent).FirstOrDefault(p => p.ID == id);
                if (pacijent != null && pacijent.Pacijent != null)
                {
                    ViewBag.PacijentKnjizica = pacijent.Pacijent.BrojZdravstveneKnjizice;
                    ViewData["PacijentInfo"] = "Ime i prezime: " + pacijent.Pacijent.Ime + " " + pacijent.Pacijent.Prezime;
                }
                
            }

            if (prijem.LjekarID == 0)
            {
                ViewBag.LjekarSifra = "";
                ViewBag.ErrorLjekarID = "Ne postoji ljekar s navedenom šifrom.";
            } else
            {
                var ljekar = _context.Prijemi.Include(item => item.Ljekar).FirstOrDefault(p => p.ID == id);
                if (ljekar != null && ljekar.Ljekar != null)
                {
                    ViewBag.LjekarSifra = ljekar.Ljekar.Sifra;
                    ViewData["LjekarInfo"] = "Ime i prezime: " + ljekar.Ljekar.Ime + " " + ljekar.Ljekar.Prezime;
                }
            }

            return View(prijem);
        }

        // GET: Prijem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prijemi == null)
            {
                return NotFound();
            }

            var prijem = await _context.Prijemi
                .Include(p => p.Ljekar)
                .Include(p => p.Pacijent)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (prijem == null)
            {
                return NotFound();
            }

            return View(prijem);
        }

        // POST: Prijem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prijemi == null)
            {
                return Problem("Entity set 'KlinikaContext.Prijemi'  is null.");
            }
            var prijem = await _context.Prijemi.FindAsync(id);
            if (prijem != null)
            {
                _context.Prijemi.Remove(prijem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrijemExists(int id)
        {
          return (_context.Prijemi?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        [HttpGet]
        public string FindPacijentByBrKnjizice(string param)
        {
            var pacijent = _context.Pacijenti.FirstOrDefaultAsync(p => p.BrojZdravstveneKnjizice == param).Result;
            if (pacijent == null)
            {
                return "Ne postoji pacijent sa ovim brojem zdravstvene knjižice.";
            }
            return pacijent.Ime + " " + pacijent.Prezime + " ID: " + pacijent.ID;
        }

        [HttpGet]
        public string FindLjekarBySifra(string param)
        {
            var ljekar = _context.Ljekari.FirstOrDefaultAsync(item => item.Sifra == param).Result;
            if(ljekar == null)
            {
                return "Ne postoji ljekar sa ovom sifrom.";
            }
            return ljekar.Ime + " " + ljekar.Prezime + " sifra: " + ljekar.ID;
        }

        [HttpGet]
        public async Task<PartialViewResult> FilterPrijemsByDate(DateTime startDate, DateTime endDate)
        {
            var filteredPrijems = await _context.Prijemi
                .Where(prijem => prijem.DatumVrijemePrijema.Date >= startDate.Date && prijem.DatumVrijemePrijema.Date <= endDate.Date)
                .Include(p => p.Ljekar)
                .Include(p => p.Pacijent)
                .ToListAsync();

            return PartialView("PrijemsTablePartialView", filteredPrijems);
        }
    }
}
