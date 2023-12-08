using MoviesAPI.Interfaces;
using System.Threading.Tasks;
using MoviesAPI.Models; // Assuming your models are in this namespace
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using MoviesAPI.DTOs;

namespace MoviesAPI.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly EmployeeContext _context;

        public PayrollRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PayrollDto>> GetAllPayrollsAsync()
        {
            var payrolls = await _context.Payrolls.ToListAsync();
            return payrolls.Select(p => new PayrollDto
            {
                PayrollId = p.PayrollId,
                EmployeeId = p.EmployeeId,
                Salary = p.Salary,
                Bonus = p.Bonus,
                Deductions = p.Deductions,
                PayDate = p.PayDate

                // Map other properties from Payroll to PayrollDto
            }).ToList();
        }

        public async Task<PayrollDto> GetPayrollByIdAsync(Guid id)
        {
            var payroll = await _context.Payrolls.FindAsync(id);
            if (payroll == null) return null;

            return new PayrollDto
            {
                PayrollId = payroll.PayrollId,
                EmployeeId = payroll.EmployeeId,
                Salary = payroll.Salary,
                Bonus = payroll.Bonus,
                Deductions = payroll.Deductions,
                PayDate = payroll.PayDate

                // Map other properties from Payroll to PayrollDto
            };
        }

        public async Task AddPayrollAsync(PayrollDto payrollDto)
        {
            var payroll = new Payroll
            {
                // Map properties from PayrollDto to Payroll
                PayrollId = payrollDto.PayrollId,
                // ...
            };
            await _context.Payrolls.AddAsync(payroll);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePayrollAsync(PayrollDto payrollDto)
        {
            var payroll = await _context.Payrolls.FindAsync(payrollDto.PayrollId);
            if (payroll == null) return;

            // Map properties from PayrollDto to Payroll
            payroll.PayrollId = payrollDto.PayrollId;
            payroll.EmployeeId = payrollDto.EmployeeId;
            payroll.Salary = payrollDto.Salary;
            payroll.Bonus = payrollDto.Bonus;
            payroll.Deductions = payrollDto.Deductions;
            payroll.PayDate = payrollDto.PayDate;

            _context.Entry(payroll).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePayrollAsync(Guid id)
        {
            var payroll = await _context.Payrolls.FindAsync(id);
            if (payroll != null)
            {
                _context.Payrolls.Remove(payroll);
                await _context.SaveChangesAsync();
            }
        }
        public bool PayrollExists(Guid id)
        {
            return _context.employees.Any(e => e.Id == id);
        }
    }
}
