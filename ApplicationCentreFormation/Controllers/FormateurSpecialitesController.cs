using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationCentreFormation.Models;

namespace ApplicationCentreFormation.Controllers
{
    public class FormateurSpecialitesController : Controller
    {
        private readonly centreFormationDbContext _context;

        public FormateurSpecialitesController(centreFormationDbContext context)
        {
            _context = context;
        }

        // GET: FormateurSpecialites
        public async Task<IActionResult> Index()
        {
            var centreFormationDbContext = _context.FormateurSpecialite.Include(f => f.Formateur).Include(f => f.Specialite);
            return View(await centreFormationDbContext.ToListAsync());
        }

        // GET: FormateurSpecialites/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formateurSpecialite = await _context.FormateurSpecialite
                .Include(f => f.Formateur)
                .Include(f => f.Specialite)
                .FirstOrDefaultAsync(m => m.FormateurId == id);
            if (formateurSpecialite == null)
            {
                return NotFound();
            }

            return View(formateurSpecialite);
        }

        // GET: FormateurSpecialites/Create
        public IActionResult Create()
        {
            ViewData["FormateurId"] = new SelectList(_context.Formateur, "Id", "Nom");
            ViewData["SpecialiteId"] = new SelectList(_context.Specialite, "Id", "Nom");
            return View();
        }

        // POST: FormateurSpecialites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormateurId,SpecialiteId")] FormateurSpecialite formateurSpecialite)
        {
            if (ModelState.IsValid)
            {
               // formateurSpecialite.FormateurId = Guid.NewGuid();
                _context.Add(formateurSpecialite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormateurId"] = new SelectList(_context.Formateur, "Id", "Nom", formateurSpecialite.FormateurId);
            ViewData["SpecialiteId"] = new SelectList(_context.Specialite, "Id", "Nom", formateurSpecialite.SpecialiteId);
            return View(formateurSpecialite);
        }

        // GET: FormateurSpecialites/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formateurSpecialite = await _context.FormateurSpecialite.FindAsync(id);
            if (formateurSpecialite == null)
            {
                return NotFound();
            }
            ViewData["FormateurId"] = new SelectList(_context.Formateur, "Id", "Cin", formateurSpecialite.FormateurId);
            ViewData["SpecialiteId"] = new SelectList(_context.Specialite, "Id", "Nom", formateurSpecialite.SpecialiteId);
            return View(formateurSpecialite);
        }

        // POST: FormateurSpecialites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FormateurId,SpecialiteId")] FormateurSpecialite formateurSpecialite)
        {
            if (id != formateurSpecialite.FormateurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formateurSpecialite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormateurSpecialiteExists(formateurSpecialite.FormateurId))
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
            ViewData["FormateurId"] = new SelectList(_context.Formateur, "Id", "Cin", formateurSpecialite.FormateurId);
            ViewData["SpecialiteId"] = new SelectList(_context.Specialite, "Id", "Nom", formateurSpecialite.SpecialiteId);
            return View(formateurSpecialite);
        }

        // GET: FormateurSpecialites/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formateurSpecialite = await _context.FormateurSpecialite
                .Include(f => f.Formateur)
                .Include(f => f.Specialite)
                .FirstOrDefaultAsync(m => m.FormateurId == id);
            if (formateurSpecialite == null)
            {
                return NotFound();
            }

            return View(formateurSpecialite);
        }

        // POST: FormateurSpecialites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var formateurSpecialite = await _context.FormateurSpecialite.FindAsync(id);
            _context.FormateurSpecialite.Remove(formateurSpecialite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormateurSpecialiteExists(Guid id)
        {
            return _context.FormateurSpecialite.Any(e => e.FormateurId == id);
        }
    }
}
