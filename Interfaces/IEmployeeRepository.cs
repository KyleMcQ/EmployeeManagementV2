using MoviesAPI.DTOs;

 
namespace MoviesAPI.Interfaces
{
    public interface IEmployeeRepository
    {
        // Method to get a single employee by ID, including related details, and return as DTO
        Task<EmployeeDto> GetEmployeeByIdWithDetailsAsync(Guid id);

        // Method to get all employees including related details, and return as a collection of DTOs
        Task<IEnumerable<EmployeeDto>> GetEmployeesWithDetailsAsync();

        // Method to add a new employee using an EmployeeDto and return the newly created EmployeeDto
        Task<EmployeeDto> AddEmployeeAsync(EmployeeDto employeeDto);

        // Method to update an existing employee using an EmployeeDto
        Task UpdateEmployeeAsync(EmployeeDto employeeDto);

        // Method to delete an employee by ID
        Task DeleteEmployeeAsync(Guid id);

        // Additional method to check if an employee exists by ID
        bool EmployeeExists(Guid id);
    }
}
