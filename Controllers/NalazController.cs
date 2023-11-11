﻿using System;
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
              return _context.Nalazi != null ? 
                          View(await _context.Nalazi.ToListAsync()) :
                          Problem("Entity set 'KlinikaContext.Nalazi'  is null.");
        }

        // GET: Nalaz/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nalazi == null)
            {
                return NotFound();
            }

            var nalaz = await _context.Nalazi
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nalaz == null)
            {
                return NotFound();
            }

            return View(nalaz);
        }

        // GET: Nalaz/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nalaz/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Opis,DatumVrijemeKreiranja")] Nalaz nalaz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nalaz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
            return View(nalaz);
        }

        // POST: Nalaz/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Opis,DatumVrijemeKreiranja")] Nalaz nalaz)
        {
            if (id != nalaz.ID)
            {
                return NotFound();
            }

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
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        private bool NalazExists(int id)
        {
          return (_context.Nalazi?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
