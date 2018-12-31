using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ncframework.Models;

namespace ncframework.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IxContext _context;

        public OrganizationController(IxContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> GetOrg()
        {
            var data = await _context.Lookup.Where(o => o.Group == "ORG").Select( o => new
            {
                o.Id,
                o.ParentId,
                o.Name
            }).ToListAsync();
            return Json(data);
        }
    }
}