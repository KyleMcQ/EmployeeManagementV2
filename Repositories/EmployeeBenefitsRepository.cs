using MoviesAPI.Interfaces;
using MoviesAPI.Models; // Replace with your actual namespace
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesAPI.DTOs;

namespace MoviesAPI.Repositories
{
    public class EmployeeBenefitsRepository : IEmployeeBenefitsRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeBenefitsRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeBenefitsDto>> GetAllEmployeeBenefitsAsync()
        {
            return await _context.EmployeeBenefits
                .Select(benefit => new EmployeeBenefitsDto
                {
                    BenefitId = benefit.BenefitId,
                    EmployeeId = benefit.EmployeeId,
                    BenefitType = benefit.BenefitType,
                    Details = benefit.Details,
                    Cost = benefit.Cost
                   
        // Map properties from EmployeeBenefits to EmployeeBenefitsDto
    })
                .ToListAsync();
        }

        public async Task<EmployeeBenefitsDto> GetEmployeeBenefitsByIdAsync(Guid id)
        {
            var benefit = await _context.EmployeeBenefits.FindAsync(id);
            if (benefit == null) return null;

            return new EmployeeBenefitsDto
            {
                BenefitId = benefit.BenefitId,
                EmployeeId = benefit.EmployeeId,
                BenefitType = benefit.BenefitType,
                Details = benefit.Details,
                Cost = benefit.Cost

                // Map properties from EmployeeBenefits to EmployeeBenefitsDto
            };
        }

        public async Task AddEmployeeBenefitsAsync(EmployeeBenefitsDto employeeBenefitsDto)
        {
            var benefit = new EmployeeBenefits
            {
                BenefitId = employeeBenefitsDto.BenefitId,
                EmployeeId = employeeBenefitsDto.EmployeeId,
                BenefitType = employeeBenefitsDto.BenefitType,
                Details = employeeBenefitsDto.Details,
                Cost = employeeBenefitsDto.Cost
                // Map properties from EmployeeBenefitsDto to EmployeeBenefits
            };
            await _context.EmployeeBenefits.AddAsync(benefit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeBenefitsAsync(EmployeeBenefitsDto employeeBenefitsDto)
        {
            var benefit = await _context.EmployeeBenefits.FindAsync(employeeBenefitsDto.BenefitId);
            if (benefit == null) return;

            benefit.BenefitId = employeeBenefitsDto.BenefitId;
            benefit.EmployeeId = employeeBenefitsDto.EmployeeId;
            benefit.BenefitType = employeeBenefitsDto.BenefitType;
            benefit.Details = employeeBenefitsDto.Details;
            benefit.Cost = employeeBenefitsDto.Cost;

            // Map properties from EmployeeBenefitsDto to EmployeeBenefits
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeBenefitsAsync(Guid id)
        {
            var benefit = await _context.EmployeeBenefits.FindAsync(id);
            if (benefit != null)
            {
                _context.EmployeeBenefits.Remove(benefit);
                await _context.SaveChangesAsync();
            }
        }
    }
}
