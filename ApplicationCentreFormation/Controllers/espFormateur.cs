using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCentreFormation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCentreFormation.Controllers
{
    public class espFormateur : Controller
    {
        private readonly centreFormationDbContext _context;

        public espFormateur(centreFormationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var centreFormationDbContext = _context.Formation.Include(f => f.Niveau);
            return View(await centreFormationDbContext.ToListAsync());
        }

    }
}
