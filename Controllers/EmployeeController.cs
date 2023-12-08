using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Interfaces;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeeDto()
        {
            var employees = await _employeeRepository.GetEmployeesWithDetailsAsync();

            if (employees == null || !employees.Any())
            {
                return NotFound();
            }

            return employees.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(Guid id)
        {
            var employeeDto = await _employeeRepository.GetEmployeeByIdWithDetailsAsync(id);

            if (employeeDto == null)
            {
                return NotFound();
            }

            return employeeDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _employeeRepository.UpdateEmployeeAsync(employeeDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_employeeRepository.EmployeeExists(id))
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

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEmployee = await _employeeRepository.AddEmployeeAsync(employeeDto);

            return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdWithDetailsAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeeRepository.DeleteEmployeeAsync(id);

            return NoContent();
        }
    }
}
