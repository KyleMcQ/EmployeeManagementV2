namespace MoviesAPI.DTOs
{
    public class PayrollDto
    {
        public Guid PayrollId { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal Salary { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? Deductions { get; set; }
        public DateTime PayDate { get; set; }

        
    }
}
