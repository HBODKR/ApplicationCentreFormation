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
    public class FormateursController : Controller
    {
        private readonly centreFormationDbContext _context;

        public FormateursController(centreFormationDbContext context)
        {
            _context = context;
        }

        // GET: Formateurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Formateur.ToListAsync());
        }

        // GET: Formateurs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formateur = await _context.Formateur
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formateur == null)
            {
                return NotFound();
            }

            return View(formateur);
        }

        // GET: Formateurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Email,Tel,Cin,TarifHoraire,Photo,Cv,MotPass")] Formateur formateur)
        {
            if (ModelState.IsValid)
            {
                formateur.Id = Guid.NewGuid();
                _context.Add(formateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formateur);
        }

        // GET: Formateurs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formateur = await _context.Formateur.FindAsync(id);
            if (formateur == null)
            {
                return NotFound();
            }
            return View(formateur);
        }

        // POST: Formateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nom,Prenom,Email,Tel,Cin,TarifHoraire,Photo,Cv,MotPass")] Formateur formateur)
        {
            if (id != formateur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormateurExists(formateur.Id))
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
            return View(formateur);
        }

        // GET: Formateurs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formateur = await _context.Formateur
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formateur == null)
            {
                return NotFound();
            }

            return View(formateur);
        }

        // POST: Formateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var formateur = await _context.Formateur.FindAsync(id);
            _context.Formateur.Remove(formateur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormateurExists(Guid id)
        {
            return _context.Formateur.Any(e => e.Id == id);
        }
    }
}
