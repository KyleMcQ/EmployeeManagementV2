using MoviesAPI.Interfaces;
using System.Threading.Tasks;
using MoviesAPI.Models; // Assuming you have a Models namespace
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly EmployeeContext _context;

        public PayrollRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payroll>> GetAllPayrollsAsync()
        {
            return await _context.Payrolls.ToListAsync();
        }

        public async Task<Payroll> GetPayrollByIdAsync(Guid id)
        {
            return await _context.Payrolls.FindAsync(id);
        }

        public async Task AddPayrollAsync(Payroll payroll)
        {
            await _context.Payrolls.AddAsync(payroll);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePayrollAsync(Payroll payroll)
        {
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
    }
}
