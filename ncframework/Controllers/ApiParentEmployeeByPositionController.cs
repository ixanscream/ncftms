using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ncframework.Models;

namespace ncframework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiParentEmployeeByPositionController : ControllerBase
    {
        private readonly IxContext _context;

        public ApiParentEmployeeByPositionController(IxContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee(string id)
        {
            var _param = string.Empty;
            var parentLookup = await _context.Lookup.FirstOrDefaultAsync(o => o.Id == id);

            if (parentLookup != null)
            {
                _param = parentLookup.ParentId;
            }

            return await _context.Employee.Where(o => o.GroupId == _param).ToListAsync();
        }

    }
}