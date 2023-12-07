﻿using EmployeeManagement.DTOs; // Make sure this is the correct namespace for your DTOs
using MoviesAPI.Models;
using MoviesAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesAPI.DTOs;

namespace MoviesAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
            context.Database.EnsureCreated();
        }

        public async Task<EmployeeDto> GetEmployeeByIdWithDetailsAsync(Guid id)
        {
            var employee = await _context.employees
                                 .Include(e => e.EmployeeJobs)
                                 .Include(e => e.Payrolls)
                                 .Include(e => e.EmployeeBenefits)
                                 .SingleOrDefaultAsync(e => e.Id == id);

            if (employee == null) return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
                Age = employee.Age,
                EmployeeJobs = employee.EmployeeJobs.Select(job => new EmployeeJobDto
                {
                    // Map the properties of EmployeeJob to EmployeeJobDto
                }).ToList(),
                Payrolls = employee.Payrolls.Select(payroll => new PayrollDto
                {
                    // Map the properties of Payroll to PayrollDto
                }).ToList(),
                EmployeeBenefits = employee.EmployeeBenefits.Select(benefit => new EmployeeBenefitsDto
                {
                    // Map the properties of EmployeeBenefits to EmployeeBenefitsDto
                }).ToList()
            };
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesWithDetailsAsync()
        {
            var employees = await _context.employees
                                 .Include(e => e.EmployeeJobs)
                                 .Include(e => e.Payrolls)
                                 .Include(e => e.EmployeeBenefits)
                                 .ToListAsync();

            return employees.Select(employee => new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
                Age = employee.Age,
                EmployeeJobs = employee.EmployeeJobs.Select(job => new EmployeeJobDto
                {
                    // Map the properties of EmployeeJob to EmployeeJobDto
                }).ToList(),
                Payrolls = employee.Payrolls.Select(payroll => new PayrollDto
                {
                    // Map the properties of Payroll to PayrollDto
                }).ToList(),
                EmployeeBenefits = employee.EmployeeBenefits.Select(benefit => new EmployeeBenefitsDto
                {
                    // Map the properties of EmployeeBenefits to EmployeeBenefitsDto
                }).ToList()
            });
        }

        public async Task<EmployeeDto> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                // Since Id is usually generated by the database, you don't need to set it here
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                DateOfBirth = employeeDto.DateOfBirth,
                Gender = employeeDto.Gender,
                Age = employeeDto.Age,
                // Don't set the navigation properties, they are managed by different endpoints
            };

            await _context.employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            employeeDto.Id = employee.Id; // Set the Id of the DTO to the new entity's Id
            return employeeDto;
        }

        public async Task UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = await _context.employees.FindAsync(employeeDto.Id);
            if (employee != null)
            {
                employee.FirstName = employeeDto.FirstName;
                employee.LastName = employeeDto.LastName;
                employee.DateOfBirth = employeeDto.DateOfBirth;
                employee.Gender = employeeDto.Gender;
                employee.Age = employeeDto.Age;
                // Don't update the navigation properties here

                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee != null)
            {
                _context.employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public bool EmployeeExists(Guid id)
        {
            return _context.employees.Any(e => e.Id == id);
        }
    }
}
