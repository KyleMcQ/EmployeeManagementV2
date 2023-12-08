using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Interfaces; // Import the namespace where IPayrollRepository is located
using MoviesAPI.Models;

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
        public async Task<ActionResult<IEnumerable<Payroll>>> GetPayrolls()
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
        public async Task<ActionResult<Payroll>> GetPayroll(Guid id)
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
        public async Task<IActionResult> PutPayroll(Guid id, Payroll payroll)
        {
            if (id != payroll.PayrollId)
            {
                return BadRequest();
            }

            try
            {
                await _payrollRepository.UpdatePayrollAsync(payroll);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PayrollExists(id))
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
        public async Task<ActionResult<Payroll>> PostPayroll(Payroll payroll)
        {
            await _payrollRepository.AddPayrollAsync(payroll);
            return CreatedAtAction("GetPayroll", new { id = payroll.PayrollId }, payroll);
        }

        // DELETE: api/Payrolls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayroll(Guid id)
        {
            var payroll = await _payrollRepository.GetPayrollByIdAsync(id);
            if (payroll == null)
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
