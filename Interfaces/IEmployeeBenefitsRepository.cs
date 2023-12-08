﻿using MoviesAPI.DTOs;


namespace MoviesAPI.Interfaces
{
    public interface IEmployeeBenefitsRepository
    {
        Task<IEnumerable<EmployeeBenefitsDto>> GetAllEmployeeBenefitsAsync();
        Task<EmployeeBenefitsDto> GetEmployeeBenefitsByIdAsync(Guid id);
        Task AddEmployeeBenefitsAsync(EmployeeBenefitsDto employeeBenefitsDto);
        Task UpdateEmployeeBenefitsAsync(EmployeeBenefitsDto employeeBenefitsDto);
        Task DeleteEmployeeBenefitsAsync(Guid id);

    }
}
