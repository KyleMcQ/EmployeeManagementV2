using MoviesAPI.Models;

namespace MoviesAPI.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByIdAsync(Guid id);
        Task<List<Employee>> GetAllAsync();
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Guid id);
        Task SaveAsync();
    }
}
