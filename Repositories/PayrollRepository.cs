﻿using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.DTOs;
using EmployeeManagement.Context;

namespace EmployeeManagement.Repositories
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

            };
        }

        public async Task AddPayrollAsync(PayrollDto payrollDto)
        {
            var payroll = new Payroll
            {
                PayrollId = payrollDto.PayrollId,
                EmployeeId = payrollDto.EmployeeId,
                Salary = payrollDto.Salary,
                Bonus = payrollDto.Bonus,
                Deductions = payrollDto.Deductions,
                PayDate = payrollDto.PayDate

            };
            await _context.Payrolls.AddAsync(payroll);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePayrollAsync(PayrollDto payrollDto)
        {
            var payroll = await _context.Payrolls.FindAsync(payrollDto.PayrollId);
            if (payroll == null) return;

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
