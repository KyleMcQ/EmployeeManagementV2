using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class EmployeeJob
    {
        [Key]
        public Guid Id { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        [ForeignKey("EmployeeID")]
        public Guid EmployeeID { get; set; }
    }

 
}
