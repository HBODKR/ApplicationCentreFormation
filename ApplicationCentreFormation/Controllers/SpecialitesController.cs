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
    public class SpecialitesController : Controller
    {
        private readonly centreFormationDbContext _context;

        public SpecialitesController(centreFormationDbContext context)
        {
            _context = context;
        }

        // GET: Specialites
        public async Task<IActionResult> Index()
        {
            return View(await _context.Specialite.ToListAsync());
        }

        // GET: Specialites/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialite = await _context.Specialite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialite == null)
            {
                return NotFound();
            }

            return View(specialite);
        }

        // GET: Specialites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specialites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom")] Specialite specialite)
        {
            if (ModelState.IsValid)
            {
                specialite.Id = Guid.NewGuid();
                _context.Add(specialite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialite);
        }

        // GET: Specialites/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialite = await _context.Specialite.FindAsync(id);
            if (specialite == null)
            {
                return NotFound();
            }
            return View(specialite);
        }

        // POST: Specialites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nom")] Specialite specialite)
        {
            if (id != specialite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialiteExists(specialite.Id))
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
            return View(specialite);
        }

        // GET: Specialites/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialite = await _context.Specialite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialite == null)
            {
                return NotFound();
            }

            return View(specialite);
        }

        // POST: Specialites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var specialite = await _context.Specialite.FindAsync(id);
            _context.Specialite.Remove(specialite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialiteExists(Guid id)
        {
            return _context.Specialite.Any(e => e.Id == id);
        }
    }
}
