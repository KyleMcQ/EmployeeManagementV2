using MoviesAPI.DTOs;

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
