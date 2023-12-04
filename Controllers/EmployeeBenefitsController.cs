using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeBenefitsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeBenefits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefits>>> GetEmployeeBenefits()
        {
          if (_context.EmployeeBenefits == null)
          {
              return NotFound();
          }
            return await _context.EmployeeBenefits.ToListAsync();
        }

        // GET: api/EmployeeBenefits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefits>> GetEmployeeBenefits(Guid id)
        {
          if (_context.EmployeeBenefits == null)
          {
              return NotFound();
          }
            var employeeBenefits = await _context.EmployeeBenefits.FindAsync(id);

            if (employeeBenefits == null)
            {
                return NotFound();
            }

            return employeeBenefits;
        }

        // PUT: api/EmployeeBenefits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefits(Guid id, EmployeeBenefits employeeBenefits)
        {
            if (id != employeeBenefits.BenefitId)
            {
                return BadRequest();
            }

            _context.Entry(employeeBenefits).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeBenefitsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmployeeBenefits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeBenefits>> PostEmployeeBenefits(EmployeeBenefits employeeBenefits)
        {
          if (_context.EmployeeBenefits == null)
          {
              return Problem("Entity set 'EmployeeContext.EmployeeBenefits'  is null.");
          }
            _context.EmployeeBenefits.Add(employeeBenefits);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeBenefits", new { id = employeeBenefits.BenefitId }, employeeBenefits);
        }

        // DELETE: api/EmployeeBenefits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefits(Guid id)
        {
            if (_context.EmployeeBenefits == null)
            {
                return NotFound();
            }
            var employeeBenefits = await _context.EmployeeBenefits.FindAsync(id);
            if (employeeBenefits == null)
            {
                return NotFound();
            }

            _context.EmployeeBenefits.Remove(employeeBenefits);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeBenefitsExists(Guid id)
        {
            return (_context.EmployeeBenefits?.Any(e => e.BenefitId == id)).GetValueOrDefault();
        }
    }
}
