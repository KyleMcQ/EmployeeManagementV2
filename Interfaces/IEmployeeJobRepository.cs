using MoviesAPI.DTOs;


namespace MoviesAPI.Interfaces
{
    public interface IEmployeeJobRepository
    {
        Task<IEnumerable<EmployeeJobDto>> GetAllEmployeeJobsAsync();
        Task<EmployeeJobDto> GetEmployeeJobByIdAsync(Guid id);
        Task AddEmployeeJobAsync(EmployeeJobDto employeeJobDto);
        Task UpdateEmployeeJobAsync(EmployeeJobDto employeeJobDto);
        Task DeleteEmployeeJobAsync(Guid id);
    }
}
