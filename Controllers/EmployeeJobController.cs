using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Interfaces; // Import the namespace of the IEmployeeJobRepository

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeJobController : ControllerBase
    {
        private readonly IEmployeeJobRepository _employeeJobRepository;

        public EmployeeJobController(IEmployeeJobRepository employeeJobRepository)
        {
            _employeeJobRepository = employeeJobRepository;
        }

        // GET: api/EmployeeJob
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeJobDto>>> GetEmployeeJobs()
        {
            var employeeJobs = await _employeeJobRepository.GetAllEmployeeJobsAsync();
            if (employeeJobs == null)
            {
                return NotFound();
            }
            return Ok(employeeJobs);
        }

        // GET: api/EmployeeJob/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeJobDto>> GetEmployeeJob(Guid id)
        {
            var employeeJob = await _employeeJobRepository.GetEmployeeJobByIdAsync(id);
            if (employeeJob == null)
            {
                return NotFound();
            }
            return Ok(employeeJob);
        }

        // PUT: api/EmployeeJob/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeJob(Guid id, EmployeeJobDto employeeJobDto)
        {
            if (id != employeeJobDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _employeeJobRepository.UpdateEmployeeJobAsync(employeeJobDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EmployeeJobExists(id))
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

        // POST: api/EmployeeJob
        [HttpPost]
        public async Task<ActionResult<EmployeeJobDto>> PostEmployeeJob(EmployeeJobDto employeeJobDto)
        {
            await _employeeJobRepository.AddEmployeeJobAsync(employeeJobDto);
            return CreatedAtAction(nameof(GetEmployeeJob), new { id = employeeJobDto.Id }, employeeJobDto);
        }

        // DELETE: api/EmployeeJob/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeJob(Guid id)
        {
            var employeeJob = await _employeeJobRepository.GetEmployeeJobByIdAsync(id);
            if (employeeJob == null)
            {
                return NotFound();
            }

            await _employeeJobRepository.DeleteEmployeeJobAsync(id);
            return NoContent();
        }

        private async Task<bool> EmployeeJobExists(Guid id)
        {
            var employeeJob = await _employeeJobRepository.GetEmployeeJobByIdAsync(id);
            return employeeJob != null;
        }
    }
}
