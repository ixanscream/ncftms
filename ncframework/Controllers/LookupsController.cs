using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ncframework.Models;
using ReflectionIT.Mvc.Paging;

namespace ncframework.Controllers
{
    public class LookupsController : Controller
    {
        private readonly IxContext _context;

        public LookupsController(IxContext context)
        {
            _context = context;
        }

        // GET: Lookups
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "Group")
        {
            var _Lookup = _context.Lookup.Include(o => o.Parent).AsNoTracking().OrderBy(x => x.Group).AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                _Lookup = _Lookup.Where(p => p.Name.Contains(filter));
            }
            var _result = await PagingList<Lookup>.CreateAsync(_Lookup, 20, page, sortExpression, sortExpression);
            _result.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(_result);
        }

        // GET: Lookups/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lookup = await _context.Lookup
                .Include(l => l.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lookup == null)
            {
                return NotFound();
            }

            return View(lookup);
        }

        // GET: Lookups/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.Lookup, "Id", "Name");
            return View();
        }

        // POST: Lookups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code,ParentId,Group")] Lookup lookup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lookup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Lookup, "Id", "Name", lookup.ParentId);
            return View(lookup);
        }

        // GET: Lookups/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lookup = await _context.Lookup.FindAsync(id);
            if (lookup == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.Lookup, "Id", "Name", lookup.ParentId);
            return View(lookup);
        }

        // POST: Lookups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Code,ParentId,Group")] Lookup lookup)
        {
            if (id != lookup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lookup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LookupExists(lookup.Id))
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
            ViewData["ParentId"] = new SelectList(_context.Lookup, "Id", "Name", lookup.ParentId);
            return View(lookup);
        }

        // GET: Lookups/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lookup = await _context.Lookup
                .Include(l => l.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lookup == null)
            {
                return NotFound();
            }

            return View(lookup);
        }

        // POST: Lookups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lookup = await _context.Lookup.FindAsync(id);
            _context.Lookup.Remove(lookup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LookupExists(string id)
        {
            return _context.Lookup.Any(e => e.Id == id);
        }
    }
}
