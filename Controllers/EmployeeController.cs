using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
            context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeeDto()
        {
            if (_context.employees == null)
            {
                return NotFound();
            }

            var employees = await _context.employees
                .Include(e => e.EmployeeJobs) // Include EmployeeJobs
                .Include(e => e.Payrolls)     // Include Payrolls
                .Include(e => e.EmployeeBenefits) // Include EmployeeBenefits
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DateOfBirth = e.DateOfBirth,
                    Gender = e.Gender,
                    Age = e.Age,
                    EmployeeJobs = e.EmployeeJobs.Select(j => new EmployeeJobDto
                    {
                        Id = j.Id,
                        EmployeeID = j.EmployeeID,
                        JobTitle = j.JobTitle,
                        Description = j.Description
                    }).ToList(),
                    Payrolls = e.Payrolls.Select(p => new PayrollDto
                    {
                        PayrollId = p.PayrollId,
                        EmployeeId = p.EmployeeId,
                        Salary = p.Salary,
                        Bonus = p.Bonus,
                        Deductions = p.Deductions,
                        PayDate = p.PayDate
                    }).ToList(),
                    EmployeeBenefits = e.EmployeeBenefits.Select(b => new EmployeeBenefitsDto
                    {
                        BenefitId = b.BenefitId,
                        EmployeeId = b.EmployeeId,
                        BenefitType = b.BenefitType,
                        Details = b.Details,
                        Cost = b.Cost
                    }).ToList()
                }).ToListAsync();

            return employees;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetDirector(Guid id)
        {
            if (_context.employees == null)
            {
                return NotFound();
            }

            var employee = await _context.employees
                                        .Include(e => e.EmployeeJobs) // Include EmployeeJobs
                                        .Include(e => e.EmployeeBenefits) // Include EmployeeBenefits
                                        .Include(e => e.Payrolls) // Include Payrolls
                                        .SingleOrDefaultAsync(e => e.Id == id); // Find by ID

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
                Age = employee.Age,
                EmployeeJobs = employee.EmployeeJobs.Select(j => new EmployeeJobDto
                {
                    Id = j.Id,
                    EmployeeID = j.EmployeeID,
                    JobTitle = j.JobTitle,
                    Description = j.Description
                }).ToList(),
                EmployeeBenefits = employee.EmployeeBenefits.Select(b => new EmployeeBenefitsDto
                {
                    BenefitId = b.BenefitId,
                    EmployeeId = b.EmployeeId,
                    BenefitType = b.BenefitType,
                    Details = b.Details,
                    Cost = b.Cost
                }).ToList(),
                Payrolls = employee.Payrolls.Select(p => new PayrollDto
                {
                    PayrollId = p.PayrollId,
                    EmployeeId = p.EmployeeId,
                    Salary = p.Salary,
                    Bonus = p.Bonus,
                    Deductions = p.Deductions,
                    PayDate = p.PayDate
                }).ToList()
            };

            return employeeDto;
        }
        // PUT: api/Directors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDirector(Guid id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
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

        // POST: api/Directors
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(Employee employee)
        {
            if (_context.employees == null)
            {
                return Problem("Entity set 'MoviesContext.employees' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDirector), new { id = employee.Id }, employee);
        }

        // DELETE: api/Directors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(Guid id)
        {
            if (_context.employees == null)
            {
                return NotFound();
            }
            var director = await _context.employees.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }

            _context.employees.Remove(director);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DirectorExists(Guid id)
        {
            return (_context.employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
