using MoviesAPI.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.DTOs
{
    public class EmployeeDto
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public ICollection<EmployeeJobDto> EmployeeJobs { get; set; } = new List<EmployeeJobDto>();
        public ICollection<PayrollDto> Payrolls { get; set; } = new List<PayrollDto>();
        public ICollection<EmployeeBenefitsDto> EmployeeBenefits { get; set; } = new List<EmployeeBenefitsDto>();



    }
}
