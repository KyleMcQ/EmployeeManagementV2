using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Interfaces; // Import the namespace where IPayrollRepository is located
using MoviesAPI.Models; // Contains PayrollDto

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollsController : ControllerBase
    {
        private readonly IPayrollRepository _payrollRepository;

        public PayrollsController(IPayrollRepository payrollRepository)
        {
            _payrollRepository = payrollRepository;
        }

        // GET: api/Payrolls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayrollDto>>> GetPayrolls()
        {
            var payrolls = await _payrollRepository.GetAllPayrollsAsync();
            if (payrolls == null)
            {
                return NotFound();
            }
            return Ok(payrolls);
        }

        // GET: api/Payrolls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayrollDto>> GetPayroll(Guid id)
        {
            var payroll = await _payrollRepository.GetPayrollByIdAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }
            return Ok(payroll);
        }

        // PUT: api/Payrolls/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayroll(Guid id, PayrollDto payrollDto)
        {
            if (id != payrollDto.PayrollId)
            {
                return BadRequest();
            }

            try
            {
                await _payrollRepository.UpdatePayrollAsync(payrollDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_payrollRepository.PayrollExists(id))
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

        // POST: api/Payrolls
        [HttpPost]
        public async Task<ActionResult<PayrollDto>> PostPayroll(PayrollDto payrollDto)
        {
            await _payrollRepository.AddPayrollAsync(payrollDto);
            return CreatedAtAction("GetPayroll", new { id = payrollDto.PayrollId }, payrollDto);
        }

        // DELETE: api/Payrolls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayroll(Guid id)
        {
            if (!await PayrollExists(id))
            {
                return NotFound();
            }

            await _payrollRepository.DeletePayrollAsync(id);
            return NoContent();
        }

        private async Task<bool> PayrollExists(Guid id)
        {
            return await _payrollRepository.GetPayrollByIdAsync(id) != null;
        }
    }
}
