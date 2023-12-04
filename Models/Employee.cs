using EmployeeManagement.DTOs;
using System.ComponentModel.DataAnnotations;
namespace MoviesAPI.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public ICollection<EmployeeJob> EmployeeJobs { get; set; } = new List<EmployeeJob>();
        public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
        public ICollection<EmployeeBenefits> EmployeeBenefits { get; set; } = new List<EmployeeBenefits>();


    }

}

