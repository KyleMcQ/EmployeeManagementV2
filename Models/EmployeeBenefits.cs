using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class EmployeeBenefits
    {
        [Key]
        public Guid BenefitId { get; set; }
        public Guid EmployeeId { get; set; }
        public string BenefitType { get; set; }
        public string Details { get; set; }
        public decimal Cost { get; set; }

    }
}
