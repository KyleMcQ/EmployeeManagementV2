using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Interfaces; 
using MoviesAPI.DTOs;
using Microsoft.EntityFrameworkCore; 

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsController : ControllerBase
    {
        private readonly IEmployeeBenefitsRepository _employeeBenefitsRepository;

        public EmployeeBenefitsController(IEmployeeBenefitsRepository employeeBenefitsRepository)
        {
            _employeeBenefitsRepository = employeeBenefitsRepository;
        }

        // GET: api/EmployeeBenefits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsDto>>> GetEmployeeBenefits()
        {
            var employeeBenefits = await _employeeBenefitsRepository.GetAllEmployeeBenefitsAsync();
            if (employeeBenefits == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefits);
        }

        // GET: api/EmployeeBenefits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsDto>> GetEmployeeBenefits(Guid id)
        {
            var employeeBenefits = await _employeeBenefitsRepository.GetEmployeeBenefitsByIdAsync(id);
            if (employeeBenefits == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefits);
        }

        // PUT: api/EmployeeBenefits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefits(Guid id, EmployeeBenefitsDto employeeBenefitsDto)
        {
            if (id != employeeBenefitsDto.BenefitId)
            {
                return BadRequest();
            }

            try
            {
                await _employeeBenefitsRepository.UpdateEmployeeBenefitsAsync(employeeBenefitsDto);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EmployeeBenefitsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/EmployeeBenefits
        [HttpPost]
        public async Task<ActionResult<EmployeeBenefitsDto>> PostEmployeeBenefits(EmployeeBenefitsDto employeeBenefitsDto)
        {
            await _employeeBenefitsRepository.AddEmployeeBenefitsAsync(employeeBenefitsDto);
            return CreatedAtAction("GetEmployeeBenefits", new { id = employeeBenefitsDto.BenefitId }, employeeBenefitsDto);
        }

        // DELETE: api/EmployeeBenefits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefits(Guid id)
        {
            if (!await EmployeeBenefitsExists(id))
            {
                return NotFound();
            }

            await _employeeBenefitsRepository.DeleteEmployeeBenefitsAsync(id);
            return NoContent();
        }

        private async Task<bool> EmployeeBenefitsExists(Guid id)
        {
            var employeeBenefits = await _employeeBenefitsRepository.GetEmployeeBenefitsByIdAsync(id);
            return employeeBenefits != null;
        }
    }
}
