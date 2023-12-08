using MoviesAPI.Models; // Assuming your models are in this namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAPI.Interfaces
{
    public interface IPayrollRepository
    {
        Task<IEnumerable<Payroll>> GetAllPayrollsAsync();
        Task<Payroll> GetPayrollByIdAsync(Guid id);
        Task AddPayrollAsync(Payroll payroll);
        Task UpdatePayrollAsync(Payroll payroll);
        Task DeletePayrollAsync(Guid id);

    }
}
