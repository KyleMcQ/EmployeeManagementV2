using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Models
{
    public class EmployeeContext : DbContext
    {
        public DbSet<EmployeeJob> employeeJobs { get; set; } = null;
        public DbSet<Employee> employees { get; set; } = null;
        public DbSet<Payroll> Payrolls { get; set; }

        public DbSet<EmployeeBenefits> EmployeeBenefits { get; set; }


        public EmployeeContext(DbContextOptions<EmployeeContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               new Employee()
               {
                   Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                   FirstName = "Alice",
                   LastName = "Johnson",
                   DateOfBirth = new DateTime(1985, 1, 15),
                   Gender = "Female",
                   Age = 38
               },
               new Employee
               {
                   Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                   FirstName = "Bob",
                   LastName = "Smith",
                   DateOfBirth = new DateTime(1990, 6, 20),
                   Gender = "Male",
                   Age = 33
               },
               new Employee
               {
                   Id = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                   FirstName = "Carol",
                   LastName = "Davis",
                   DateOfBirth = new DateTime(1978, 12, 2),
                   Gender = "Female",
                   Age = 45
               },
               new Employee
               {
                   Id = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
                   FirstName = "David",
                   LastName = "Lee",
                   DateOfBirth = new DateTime(1982, 3, 14),
                   Gender = "Male",
                   Age = 41
               },
               new Employee
               {
                   Id = Guid.Parse("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"),
                   FirstName = "Eva",
                   LastName = "Martinez",
                   DateOfBirth = new DateTime(1992, 11, 30),
                   Gender = "Female",
                   Age = 31
               },
               new Employee
               {
                   Id = Guid.Parse("2aadd2df-7caf-45ab-9355-7f6332985a87"),
                   FirstName = "Frank",
                   LastName = "Garcia",
                   DateOfBirth = new DateTime(1975, 4, 9),
                   Gender = "Male",
                   Age = 48
               },
               new Employee
               {
                   Id = Guid.Parse("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"),
                   FirstName = "Grace",
                   LastName = "Kim",
                   DateOfBirth = new DateTime(1988, 10, 23),
                   Gender = "Female",
                   Age = 35
               }
            );
            modelBuilder.Entity<Payroll>().HasData(
                new Payroll
                {
                    PayrollId = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                    EmployeeId = Guid.Parse("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"),
                    Salary = 50000,
                    Bonus = 10000,
                    Deductions = 5000,
                    PayDate = new DateTime(2021, 1, 15)
                },
                new Payroll
                {
                    PayrollId = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                    EmployeeId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Salary = 60000,
                    Bonus = 12000,
                    Deductions = 6000,
                    PayDate = new DateTime(2021, 1, 15)
                },
                new Payroll
                {
                    PayrollId = Guid.Parse("d173e20d-159e-4127-9ce9-b0ac2564ad97"),
                    EmployeeId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    Salary = 70000,
                    Bonus = 14000,
                    Deductions = 7000,
                    PayDate = new DateTime(2021, 1, 15)
                },
                new Payroll
                {
                    PayrollId = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
                    EmployeeId = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                    Salary = 80000,
                    Bonus = 16000,
                    Deductions = 8000,
                    PayDate = new DateTime(2021, 1, 15)
                }               
                );
            modelBuilder.Entity<EmployeeBenefits>().HasData(
            new EmployeeBenefits
            {
                BenefitId = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-1234567890ab"),
                EmployeeId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), 
                BenefitType = "Health Insurance",
                Details = "Full coverage medical and dental insurance",
                Cost = 300.00M
            },
    new EmployeeBenefits
    {
        BenefitId = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-2345678901ab"),
        EmployeeId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
        BenefitType = "Retirement Plan",
        Details = "Company matched 401(k) retirement plan",
        Cost = 200.00M
    },
    new EmployeeBenefits
    {
        BenefitId = Guid.Parse("c3d4e5f6-a789-0123-cdef-3456789012ab"),
        EmployeeId = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"), 
        BenefitType = "Life Insurance",
        Details = "Term life insurance policy",
        Cost = 100.00M
    }

            );

        modelBuilder.Entity<EmployeeJob>().HasData(
                new EmployeeJob
                {
                    Id = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                    EmployeeID = Guid.Parse("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"),
                    JobTitle = "Marketing Coordinator",
                    Description = "Develops and implements marketing strategies"
                },
                new EmployeeJob
                {
                    Id = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                    EmployeeID = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    JobTitle = "Software Developer",
                    Description = "Designs and maintains software applications"
                },
                new EmployeeJob
                {
                    Id = Guid.Parse("d173e20d-159e-4127-9ce9-b0ac2564ad97"),
                    EmployeeID = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    JobTitle = "Human Resources Manager",
                    Description = "Manages employee relations and recruitment processes"
                },
                new EmployeeJob
                {
                    Id = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
                    EmployeeID = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                    JobTitle = "Accountant",
                    Description = "Oversees financial records and budgeting processes"
                }
                );


            base.OnModelCreating(modelBuilder);
        }
    }
}
