using MoviesAPI.Models;

namespace MoviesAPI.DTOs
{
    public class EmployeeJobDto
    {
        public Guid Id { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }

        public Guid EmployeeID { get; set; }
    }
}
