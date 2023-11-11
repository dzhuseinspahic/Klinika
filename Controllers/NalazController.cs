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
    public class NalazController : Controller
    {
        private readonly KlinikaContext _context;

        public NalazController(KlinikaContext context)
        {
            _context = context;
        }

        // GET: Nalaz
        public async Task<IActionResult> Index()
        {
            var klinikaContext = _context.Nalazi.Include(n => n.Prijem);
            return View(await klinikaContext.ToListAsync());
        }

        // GET: Nalaz/Details/5
        public async Task<IActionResult> Details(int? id) 
        {
            if (id == null || _context.Nalazi == null)
            {
                return NotFound();
            }

            var nalaz = await _context.Nalazi
                .Include(n => n.Prijem)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nalaz == null)
            {
                return NotFound();
            }

            return View(nalaz);
        }

        // GET: Nalaz/Create
        public async Task<IActionResult> Create(int prijemID)
        {
            //check if one Nalaz for this Prijem is already created
            var nalazExists = await _context.Nalazi.Include(p => p.Prijem).FirstOrDefaultAsync(p => p.PrijemID == prijemID);
            //.Prijemi.Include(n => n.Nalaz).FirstOrDefaultAsync(p => p.);

            if (nalazExists == null)
            {
                var nalaz = new Nalaz
                {
                    PrijemID = prijemID,
                };
                return View(nalaz);

            }
            return RedirectToAction("Edit", "Nalaz", new { id = nalazExists.ID });
            //return RedirectToAction("Details", "Prijem", new { id = prijemID });
        }

        // POST: Nalaz/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Opis,DatumVrijemeKreiranja,PrijemID")] Nalaz nalaz)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(nalaz);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Nalaz", new { id = nalaz.ID });
                }
                catch (DbUpdateException d)
                {
                    return RedirectToAction("Detail", "Prijem", new { id = nalaz.PrijemID });
                }
            }
            return View(nalaz);
        }

        // GET: Nalaz/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nalazi == null)
            {
                return NotFound();
            }

            var nalaz = await _context.Nalazi.FindAsync(id);
            if (nalaz == null)
            {
                return NotFound();
            }
            ViewData["PrijemID"] = new SelectList(_context.Prijemi, "ID", "ID", nalaz.PrijemID);
            return View(nalaz);
        }

        // POST: Nalaz/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Opis,DatumVrijemeKreiranja,PrijemID")] Nalaz nalaz)
        {
            if (id != nalaz.ID)
            {
                return NotFound();
            }

            var prijem = _context.Prijemi.FirstOrDefault(p => p.ID == nalaz.PrijemID);
            nalaz.Prijem = prijem;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nalaz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NalazExists(nalaz.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Prijem", new { id = nalaz.PrijemID });
            }
            return View(nalaz);
        }

        // GET: Nalaz/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nalazi == null)
            {
                return NotFound();
            }

            var nalaz = await _context.Nalazi
                .Include(n => n.Prijem)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nalaz == null)
            {
                return NotFound();
            }

            return View(nalaz);
        }

        // POST: Nalaz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nalazi == null)
            {
                return Problem("Entity set 'KlinikaContext.Nalazi'  is null.");
            }
            var nalaz = await _context.Nalazi.FindAsync(id);
            if (nalaz != null)
            {
                _context.Nalazi.Remove(nalaz);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Prijem", new { id = nalaz.PrijemID });
        }

        private bool NalazExists(int id)
        {
          return (_context.Nalazi?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
