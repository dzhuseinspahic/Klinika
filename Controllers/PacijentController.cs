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
    public class PacijentController : Controller
    {
        private readonly KlinikaContext _context;

        public PacijentController(KlinikaContext context)
        {
            _context = context;
        }

        // GET: Pacijent
        public async Task<IActionResult> Index()
        {
              return _context.Pacijenti != null ? 
                          View(await _context.Pacijenti.ToListAsync()) :
                          Problem("Entity set 'KlinikaContext.Pacijenti'  is null.");
        }

        // GET: Pacijent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pacijenti == null)
            {
                return NotFound();
            }

            var pacijent = await _context.Pacijenti
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pacijent == null)
            {
                return NotFound();
            }

            return View(pacijent);
        }

        // GET: Pacijent/Create
        public IActionResult Create()
        {
            Pacijent p = new Pacijent
            {
                Spol = Spol.Nepoznato
            };
            return View(p);
        }

        // POST: Pacijent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Ime,Prezime,Spol,Adresa,BrojTelefona,BrojZdravstveneKnjizice")] Pacijent pacijent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacijent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pacijent);
        }

        // GET: Pacijent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pacijenti == null)
            {
                return NotFound();
            }

            var pacijent = await _context.Pacijenti.FindAsync(id);
            if (pacijent == null)
            {
                return NotFound();
            }
            return View(pacijent);
        }

        // POST: Pacijent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Ime,Prezime,Spol,Adresa,BrojTelefona,BrojZdravstveneKnjizice")] Pacijent pacijent)
        {
            if (id != pacijent.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacijent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacijentExists(pacijent.ID))
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
            return View(pacijent);
        }

        // GET: Pacijent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pacijenti == null)
            {
                return NotFound();
            }

            var pacijent = await _context.Pacijenti
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pacijent == null)
            {
                return NotFound();
            }

            return View(pacijent);
        }

        // POST: Pacijent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pacijenti == null)
            {
                return Problem("Entity set 'KlinikaContext.Pacijenti'  is null.");
            }
            var pacijent = await _context.Pacijenti.FindAsync(id);
            if (pacijent != null)
            {
                _context.Pacijenti.Remove(pacijent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacijentExists(int id)
        {
          return (_context.Pacijenti?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
