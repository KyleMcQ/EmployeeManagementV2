using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.DTOs;
using EmployeeManagement.Context;

namespace EmployeeManagement.Repositories
{
    public class EmployeeJobRepository : IEmployeeJobRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeJobRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeJobDto>> GetAllEmployeeJobsAsync()
        {
            return await _context.employeeJobs.Select(job => new EmployeeJobDto
            {
                Id = job.Id,    
                JobTitle = job.JobTitle,
                Description = job.Description,
                EmployeeID = job.EmployeeID


            }).ToListAsync();
        }

        public async Task<EmployeeJobDto> GetEmployeeJobByIdAsync(Guid id)
        {
            var job = await _context.employeeJobs.FindAsync(id);
            if (job == null) return null;

            return new EmployeeJobDto
            {
                Id = job.Id,
                JobTitle = job.JobTitle,
                Description = job.Description,
                EmployeeID = job.EmployeeID
            };
        }

        public async Task AddEmployeeJobAsync(EmployeeJobDto employeeJobDto)
        {
            var job = new EmployeeJob
            {
                Id = employeeJobDto.Id,
                JobTitle = employeeJobDto.JobTitle,
                Description = employeeJobDto.Description,
                EmployeeID = employeeJobDto.EmployeeID
            };
            await _context.employeeJobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeJobAsync(EmployeeJobDto employeeJobDto)
        {
            var job = await _context.employeeJobs.FindAsync(employeeJobDto.Id);
            if (job != null)
            {
                job.JobTitle = employeeJobDto.JobTitle;
                job.JobTitle = employeeJobDto.JobTitle;
                job.Description = employeeJobDto.Description;
                job.EmployeeID = employeeJobDto.EmployeeID;

                _context.Entry(job).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteEmployeeJobAsync(Guid id)
        {
            var job = await _context.employeeJobs.FindAsync(id);
            if (job != null)
            {
                _context.employeeJobs.Remove(job);
                await _context.SaveChangesAsync();
            }
        }
    }
}
