using Microsoft.EntityFrameworkCore;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;

namespace MoviesAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            return await _context.employees.FindAsync(id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.employees.ToListAsync();
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.employees.AddAsync(employee);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.employees.Update(employee);
        }

        public async Task DeleteAsync(Guid id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee != null)
            {
                _context.employees.Remove(employee);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
