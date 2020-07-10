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
    public class NiveauxController : Controller
    {
        private readonly centreFormationDbContext _context;

        public NiveauxController(centreFormationDbContext context)
        {
            _context = context;
        }

        // GET: Niveaux
        public async Task<IActionResult> Index()
        {
            return View(await _context.Niveau.ToListAsync());
        }

        // GET: Niveaux/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var niveau = await _context.Niveau
                .FirstOrDefaultAsync(m => m.Id == id);
            if (niveau == null)
            {
                return NotFound();
            }

            return View(niveau);
        }

        // GET: Niveaux/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Niveaux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom")] Niveau niveau)
        {
            if (ModelState.IsValid)
            {
                niveau.Id = Guid.NewGuid();
                _context.Add(niveau);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(niveau);
        }

        // GET: Niveaux/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var niveau = await _context.Niveau.FindAsync(id);
            if (niveau == null)
            {
                return NotFound();
            }
            return View(niveau);
        }

        // POST: Niveaux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nom")] Niveau niveau)
        {
            if (id != niveau.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(niveau);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NiveauExists(niveau.Id))
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
            return View(niveau);
        }

        // GET: Niveaux/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var niveau = await _context.Niveau
                .FirstOrDefaultAsync(m => m.Id == id);
            if (niveau == null)
            {
                return NotFound();
            }

            return View(niveau);
        }

        // POST: Niveaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var niveau = await _context.Niveau.FindAsync(id);
            _context.Niveau.Remove(niveau);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NiveauExists(Guid id)
        {
            return _context.Niveau.Any(e => e.Id == id);
        }
    }
}
