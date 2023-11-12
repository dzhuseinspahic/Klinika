using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
                }
                else if (prijem.LjekarID != 0)
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

            var errorSpecijalista = false;

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
                }
                else
                {
                    ViewBag.ErrorLjekarID = "Možete izabrati samo ljekare sa titulom \"Specijalista\".";
                    errorSpecijalista = true;
                }

            }

            if (prijem.PacijentID == 0)
            {
                ViewBag.PacijentKnjizica = "";
                ViewBag.ErrorPacijentID = "Ne postoji pacijent s ovim brojem zdravstvene knjižice.";
            }
            else
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
                ViewBag.ErrorLjekarID = "Ne postoji ljekar s ovom šifrom.";
            }
            else if (!errorSpecijalista)
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
            if (ljekar == null)
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

        public async Task<ActionResult> GeneratePdf(int prijemID)
        {
            var prijem = await _context.Prijemi
                .Include(p => p.Ljekar)
                .Include(p => p.Pacijent)
                .Include(p => p.Nalaz)
                .FirstOrDefaultAsync(p => p.ID == prijemID);

            MemoryStream ms = new MemoryStream();
            Document doc = new Document();

            PdfWriter writer = PdfWriter.GetInstance(doc, ms);

            doc.Open();
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            Font subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);

            Paragraph titlePrijem = new Paragraph("Prijem", titleFont);
            titlePrijem.Alignment = Element.ALIGN_CENTER;
            titlePrijem.SpacingAfter = 30f;
            doc.Add(titlePrijem);

            string pdfName = "Prijem.pdf";

            if (prijem != null)
            {
                Paragraph dateParagraph = new Paragraph();

                Phrase datePhrase = new Phrase();
                datePhrase.Add(new Chunk("Datum i vrijeme prijema: ", boldFont));
                datePhrase.Add(new Chunk(prijem.DatumVrijemePrijema.ToString(), normalFont));
                dateParagraph.Add(datePhrase);
                dateParagraph.SpacingAfter = 10f;
                doc.Add(dateParagraph);

                if (prijem.Pacijent != null)
                {
                    Pacijent pacijent = prijem.Pacijent;
                    pdfName = pacijent.Ime + pacijent.Prezime + "_Prijem.pdf";

                    Paragraph titlePacijent = new Paragraph("Podaci o pacijentu", subtitleFont);
                    titlePacijent.SpacingAfter = 5f;
                    doc.Add(titlePacijent);

                    Paragraph pacijentParagraph = new Paragraph();
                    
                    Phrase imePhrase = new Phrase();
                    imePhrase.Add(new Chunk("Ime i prezime: ", boldFont));
                    imePhrase.Add(new Chunk(pacijent.Ime + " " + pacijent.Prezime, normalFont));
                    pacijentParagraph.Add(imePhrase);
                    pacijentParagraph.Add("\n");

                    Phrase spolPhrase = new Phrase();
                    spolPhrase.Add(new Chunk("Spol: ", boldFont));
                    spolPhrase.Add(new Chunk(pacijent.Spol.ToString(), normalFont));
                    pacijentParagraph.Add(spolPhrase);
                    pacijentParagraph.Add("\n");

                    if (pacijent.Adresa != null)
                    {
                        Phrase adresaPhrase = new Phrase();
                        adresaPhrase.Add(new Chunk("Adresa: ", boldFont));
                        adresaPhrase.Add(new Chunk(pacijent.Adresa, normalFont));
                        pacijentParagraph.Add(adresaPhrase);
                        pacijentParagraph.Add("\n");
                    }

                    if (pacijent.BrojTelefona != null)
                    {
                        Phrase brTelPhrase = new Phrase();
                        brTelPhrase.Add(new Chunk("Broj telefona: ", boldFont));
                        brTelPhrase.Add(new Chunk(pacijent.BrojTelefona, normalFont));
                        pacijentParagraph.Add(brTelPhrase);
                        pacijentParagraph.Add("\n");
                    }

                    Phrase knjizicaPhrase = new Phrase();
                    knjizicaPhrase.Add(new Chunk("Broj zdravstvene knjižice: ", boldFont));
                    knjizicaPhrase.Add(new Chunk(pacijent.BrojZdravstveneKnjizice, normalFont));
                    pacijentParagraph.Add(knjizicaPhrase);

                    pacijentParagraph.SpacingAfter = 20f;

                    doc.Add(pacijentParagraph);
                }
                
                if (prijem.Ljekar != null)
                {
                    Ljekar ljekar = prijem.Ljekar;
                    Paragraph ljekarParagraph = new Paragraph();
                    ljekarParagraph.Add("\n");
                    Phrase ljekarPhrase = new Phrase();
                    ljekarPhrase.Add(new Chunk("Nadležni ljekar: ", boldFont));
                    ljekarPhrase.Add(new Chunk(ljekar.Ime + " " + ljekar.Prezime + " - " + ljekar.Sifra, normalFont));
                    ljekarParagraph.Add(ljekarPhrase);

                    ljekarParagraph.SpacingAfter = 10f;
                    doc.Add(ljekarParagraph);
                }

                if (prijem.Nalaz != null)
                {
                    Paragraph titleNalaz = new Paragraph("Nalaz", titleFont);
                    titleNalaz.Alignment = Element.ALIGN_CENTER;
                    titleNalaz.SpacingAfter = 10f;
                    doc.Add(titleNalaz);

                    Nalaz nalaz = prijem.Nalaz;
                    Paragraph nalazParagraph = new Paragraph();

                    Phrase nalazPhrase = new Phrase();
                    nalazPhrase.Add(new Chunk("Opis: ", boldFont));
                    nalazPhrase.Add(new Chunk(nalaz.Opis, normalFont));
                    nalazParagraph.Add(nalazPhrase);
                    nalazParagraph.Add("\n");

                    Phrase datiNalazPhrase = new Phrase();
                    datiNalazPhrase.Add(new Chunk("Datum i vrijeme: ", boldFont));
                    datiNalazPhrase.Add(new Chunk(nalaz.DatumVrijemeKreiranja.ToString(), normalFont));
                    nalazParagraph.Add(datiNalazPhrase);
                    nalazParagraph.Add("\n");

                    doc.Add(nalazParagraph);
                }
            }
            

            doc.Close();

            
            byte[] pdfBytes = ms.ToArray();
            return File(pdfBytes, "application/pdf", pdfName);
        }
    }
}
;