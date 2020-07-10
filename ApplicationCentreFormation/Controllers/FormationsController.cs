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
    public class FormationsController : Controller
    {
        private readonly centreFormationDbContext _context;

        public FormationsController(centreFormationDbContext context)
        {
            _context = context;
        }

        // GET: Formations
        public async Task<IActionResult> Index()
        {
            var centreFormationDbContext = _context.Formation.Include(f => f.Niveau);
            return View(await centreFormationDbContext.ToListAsync());
        }

        // GET: Formations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formation = await _context.Formation
                .Include(f => f.Niveau)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formation == null)
            {
                return NotFound();
            }

            return View(formation);
        }

        // GET: Formations/Create
        public IActionResult Create()
        {
            ViewData["NiveauId"] = new SelectList(_context.Niveau, "Id", "Nom");
            return View();
        }

        // POST: Formations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titre,Description,ChargeHoraire,Programme,NiveauId")] Formation formation)
        {
            if (ModelState.IsValid)
            {
                formation.Id = Guid.NewGuid();
                _context.Add(formation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NiveauId"] = new SelectList(_context.Niveau, "Id", "Nom", formation.NiveauId);
            return View(formation);
        }

        // GET: Formations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formation = await _context.Formation.FindAsync(id);
            if (formation == null)
            {
                return NotFound();
            }
            ViewData["NiveauId"] = new SelectList(_context.Niveau, "Id", "Nom", formation.NiveauId);
            return View(formation);
        }

        // POST: Formations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Titre,Description,ChargeHoraire,Programme,NiveauId")] Formation formation)
        {
            if (id != formation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormationExists(formation.Id))
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
            ViewData["NiveauId"] = new SelectList(_context.Niveau, "Id", "Nom", formation.NiveauId);
            return View(formation);
        }

        // GET: Formations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formation = await _context.Formation
                .Include(f => f.Niveau)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formation == null)
            {
                return NotFound();
            }

            return View(formation);
        }

        // POST: Formations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var formation = await _context.Formation.FindAsync(id);
            _context.Formation.Remove(formation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormationExists(Guid id)
        {
            return _context.Formation.Any(e => e.Id == id);
        }
    }
}
