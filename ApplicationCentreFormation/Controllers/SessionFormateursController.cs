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
    public class SessionFormateursController : Controller
    {
        private readonly centreFormationDbContext _context;

        public SessionFormateursController(centreFormationDbContext context)
        {
            _context = context;
        }

        // GET: SessionFormateurs
        public async Task<IActionResult> Index()
        {
            var centreFormationDbContext = _context.SessionFormateur.Include(s => s.Formateur).Include(s => s.Session);
            return View(await centreFormationDbContext.ToListAsync());
        }

        // GET: SessionFormateurs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionFormateur = await _context.SessionFormateur
                .Include(s => s.Formateur)
                .Include(s => s.Session)
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (sessionFormateur == null)
            {
                return NotFound();
            }

            return View(sessionFormateur);
        }

        // GET: SessionFormateurs/Create
        public IActionResult Create()
        {
            ViewData["FormateurId"] = new SelectList(_context.Formateur, "Id", "Nom");
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Planning");
            return View();
        }

        // POST: SessionFormateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SessionId,FormateurId")] SessionFormateur sessionFormateur)
        {
            if (ModelState.IsValid)
            {
                //sessionFormateur.SessionId = Guid.NewGuid();
                _context.Add(sessionFormateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormateurId"] = new SelectList(_context.Formateur, "Id", "Nom", sessionFormateur.FormateurId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Planning", sessionFormateur.SessionId);
            return View(sessionFormateur);
        }

        // GET: SessionFormateurs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionFormateur = await _context.SessionFormateur.FindAsync(id);
            if (sessionFormateur == null)
            {
                return NotFound();
            }
            ViewData["FormateurId"] = new SelectList(_context.Formateur, "Id", "Cin", sessionFormateur.FormateurId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Planning", sessionFormateur.SessionId);
            return View(sessionFormateur);
        }

        // POST: SessionFormateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SessionId,FormateurId")] SessionFormateur sessionFormateur)
        {
            if (id != sessionFormateur.SessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessionFormateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionFormateurExists(sessionFormateur.SessionId))
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
            ViewData["FormateurId"] = new SelectList(_context.Formateur, "Id", "Cin", sessionFormateur.FormateurId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Planning", sessionFormateur.SessionId);
            return View(sessionFormateur);
        }

        // GET: SessionFormateurs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionFormateur = await _context.SessionFormateur
                .Include(s => s.Formateur)
                .Include(s => s.Session)
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (sessionFormateur == null)
            {
                return NotFound();
            }

            return View(sessionFormateur);
        }

        // POST: SessionFormateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sessionFormateur = await _context.SessionFormateur.FindAsync(id);
            _context.SessionFormateur.Remove(sessionFormateur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionFormateurExists(Guid id)
        {
            return _context.SessionFormateur.Any(e => e.SessionId == id);
        }
    }
}
