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
    public class LjekarController : Controller
    {
        private readonly KlinikaContext _context;

        public LjekarController(KlinikaContext context)
        {
            _context = context;
        }

        // GET: Ljekar
        public async Task<IActionResult> Index()
        {
              return _context.Ljekari != null ? 
                          View(await _context.Ljekari.ToListAsync()) :
                          Problem("Entity set 'KlinikaContext.Ljekari'  is null.");
        }

        // GET: Ljekar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ljekari == null)
            {
                return NotFound();
            }

            var ljekar = await _context.Ljekari
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ljekar == null)
            {
                return NotFound();
            }

            return View(ljekar);
        }

        // GET: Ljekar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ljekar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Ime,Prezime,Titula,Sifra")] Ljekar ljekar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ljekar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ljekar);
        }

        // GET: Ljekar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ljekari == null)
            {
                return NotFound();
            }

            var ljekar = await _context.Ljekari.FindAsync(id);
            if (ljekar == null)
            {
                return NotFound();
            }
            return View(ljekar);
        }

        // POST: Ljekar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Ime,Prezime,Titula,Sifra")] Ljekar ljekar)
        {
            if (id != ljekar.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ljekar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LjekarExists(ljekar.ID))
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
            return View(ljekar);
        }

        // GET: Ljekar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ljekari == null)
            {
                return NotFound();
            }

            var ljekar = await _context.Ljekari
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ljekar == null)
            {
                return NotFound();
            }

            return View(ljekar);
        }

        // POST: Ljekar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ljekari == null)
            {
                return Problem("Entity set 'KlinikaContext.Ljekari'  is null.");
            }
            var ljekar = await _context.Ljekari.FindAsync(id);
            if (ljekar != null)
            {
                _context.Ljekari.Remove(ljekar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LjekarExists(int id)
        {
          return (_context.Ljekari?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
