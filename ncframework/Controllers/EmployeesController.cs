﻿using System;
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
    public class EmployeesController : Controller
    {
        private readonly IxContext _context;

        public EmployeesController(IxContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "Group.Name")
        {
            var _Employee = _context.Employee.Include(o => o.Group).Include(a => a.Group).Include(a => a.Parent).AsNoTracking().OrderBy(x => x.Group.Name).AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                _Employee = _Employee.Where(p => p.Group.Name.Contains(filter));
            }
            var _result = await PagingList<Employee>.CreateAsync(_Employee, 20, page, sortExpression, sortExpression);
            _result.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(_result);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Group)
                .Include(e => e.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Lookup.Where(o => o.Group == "ORG"), "Id", "Name");
            ViewData["ParentId"] = new SelectList(_context.Employee, "Id", "Name");
            ViewData["GroupMenu"] = new SelectList(_context.Lookup.Where(o => o.Group == "ROLE"), "Code", "Code");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code,Email,Photo,Phone,ParentId,GroupId,GroupMenu")] Employee employee, string[] roles)
        {
            if (ModelState.IsValid)
            {
                employee.GroupMenu = string.Join(",", roles);
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Lookup.Where(o => o.Group == "ORG"), "Id", "Name", employee.GroupId);
            ViewData["ParentId"] = new SelectList(_context.Employee, "Id", "Name", employee.ParentId);
            ViewData["GroupMenu"] = new MultiSelectList(_context.Lookup.Where(o => o.Group == "ROLE"), "Code", "Code", employee.GroupMenu);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Lookup.Where(o => o.Group == "ORG"), "Id", "Name", employee.GroupId);
            ViewData["ParentId"] = new SelectList(_context.Employee, "Id", "Name", employee.ParentId);
            ViewData["GroupMenu"] = new MultiSelectList(_context.Lookup.Where(o => o.Group == "ROLE"), "Code", "Code", employee.GroupMenu.Split(","));
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Code,Email,Photo,Phone,ParentId,GroupId,GroupMenu")] Employee employee, string[] roles)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employee.GroupMenu = string.Join(",", roles);
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Lookup.Where(o => o.Group == "ORG"), "Id", "Name", employee.GroupId);
            ViewData["ParentId"] = new SelectList(_context.Employee, "Id", "Name", employee.ParentId);
            ViewData["GroupMenu"] = new SelectList(_context.Lookup.Where(o => o.Group == "ROLE"), "Code", "Code", employee.GroupMenu);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Group)
                .Include(e => e.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(string id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
