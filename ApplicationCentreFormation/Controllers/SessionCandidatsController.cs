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
    public class SessionCandidatsController : Controller
    {
        private readonly centreFormationDbContext _context;

        public SessionCandidatsController(centreFormationDbContext context)
        {
            _context = context;
        }

        // GET: SessionCandidats
        public async Task<IActionResult> Index()
        {
            var centreFormationDbContext = _context.SessionCandidat.Include(s => s.Candidat).Include(s => s.Session);
            return View(await centreFormationDbContext.ToListAsync());
        }

        // GET: SessionCandidats/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionCandidat = await _context.SessionCandidat
                .Include(s => s.Candidat)
                .Include(s => s.Session)
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (sessionCandidat == null)
            {
                return NotFound();
            }

            return View(sessionCandidat);
        }

        // GET: SessionCandidats/Create
        public IActionResult Create()
        {
            ViewData["CandidatId"] = new SelectList(_context.Candidat, "Id", "Nom");
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Planning");
            return View();
        }

        // POST: SessionCandidats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SessionId,CandidatId")] SessionCandidat sessionCandidat)
        {
            if (ModelState.IsValid)
            {
                //sessionCandidat.SessionId = Guid.NewGuid();
                _context.Add(sessionCandidat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CandidatId"] = new SelectList(_context.Candidat, "Id", "Nom", sessionCandidat.CandidatId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Planning", sessionCandidat.SessionId);
            return View(sessionCandidat);
        }

        // GET: SessionCandidats/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionCandidat = await _context.SessionCandidat.FindAsync(id);
            if (sessionCandidat == null)
            {
                return NotFound();
            }
            ViewData["CandidatId"] = new SelectList(_context.Candidat, "Id", "Nom", sessionCandidat.CandidatId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Planning", sessionCandidat.SessionId);
            return View(sessionCandidat);
        }

        // POST: SessionCandidats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SessionId,CandidatId")] SessionCandidat sessionCandidat)
        {
            if (id != sessionCandidat.SessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessionCandidat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionCandidatExists(sessionCandidat.SessionId))
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
            ViewData["CandidatId"] = new SelectList(_context.Candidat, "Id", "Nom", sessionCandidat.CandidatId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Planning", sessionCandidat.SessionId);
            return View(sessionCandidat);
        }

        // GET: SessionCandidats/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionCandidat = await _context.SessionCandidat
                .Include(s => s.Candidat)
                .Include(s => s.Session)
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (sessionCandidat == null)
            {
                return NotFound();
            }

            return View(sessionCandidat);
        }

        // POST: SessionCandidats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sessionCandidat = await _context.SessionCandidat.FindAsync(id);
            _context.SessionCandidat.Remove(sessionCandidat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionCandidatExists(Guid id)
        {
            return _context.SessionCandidat.Any(e => e.SessionId == id);
        }
    }
}
