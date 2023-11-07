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

        public PrijemController(KlinikaContext context)
        {
            _context = context;
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
            ViewData["LjekarID"] = new SelectList(_context.Ljekari, "ID", "Ime");
            ViewData["PacijentID"] = new SelectList(_context.Pacijenti, "ID", "Ime");
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
                _context.Add(prijem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LjekarID"] = new SelectList(_context.Ljekari, "ID", "Ime", prijem.LjekarID);
            ViewData["PacijentID"] = new SelectList(_context.Pacijenti, "ID", "Ime", prijem.PacijentID);
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
            ViewData["LjekarID"] = new SelectList(_context.Ljekari, "ID", "Ime", prijem.LjekarID);
            ViewData["PacijentID"] = new SelectList(_context.Pacijenti, "ID", "Ime", prijem.PacijentID);
            return View(prijem);
        }

        // POST: Prijem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DatumVrijemePrijema,PacijentID,LjekarID,HitniPrijem")] Prijem prijem)
        {
            if (id != prijem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
            }
            ViewData["LjekarID"] = new SelectList(_context.Ljekari, "ID", "Ime", prijem.LjekarID);
            ViewData["PacijentID"] = new SelectList(_context.Pacijenti, "ID", "Ime", prijem.PacijentID);
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
    }
}
