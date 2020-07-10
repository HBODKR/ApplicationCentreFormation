using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCentreFormation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCentreFormation.Controllers
{
    [Authorize (Roles = "Admin")]
    public class espCandidat : Controller
    {

        private readonly centreFormationDbContext _context;

        public espCandidat(centreFormationDbContext context)
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
