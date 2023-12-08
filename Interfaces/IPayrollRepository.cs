using MoviesAPI.DTOs;
using MoviesAPI.Models; // Assuming your models are in this namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAPI.Interfaces
{
    public interface IPayrollRepository
    {
        Task<IEnumerable<PayrollDto>> GetAllPayrollsAsync();
        Task<PayrollDto> GetPayrollByIdAsync(Guid id);
        Task AddPayrollAsync(PayrollDto payrollDto);
        Task UpdatePayrollAsync(PayrollDto payrollDto);
        Task DeletePayrollAsync(Guid id);

        bool PayrollExists(Guid id);

    }
}
