using MoviesAPI.DTOs;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using AutoMapper;

namespace MoviesAPI.Service
{

    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper; // Assuming you're using AutoMapper for object-object mapping.

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return null;
            }
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }

        // Other methods for update, delete, etc.
    }

}
