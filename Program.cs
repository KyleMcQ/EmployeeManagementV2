using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using MoviesAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<EmployeeContext>(opt => opt.UseInMemoryDatabase("MoviesList"));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPayrollRepository, PayrollRepository>(); // Make sure you have a PayrollRepository class that implements IPayrollRepository
builder.Services.AddScoped<IEmployeeJobRepository, EmployeeJobRepository>(); // Make sure you have a PayrollRepository class that implements IPayrollRepository
builder.Services.AddScoped<IEmployeeBenefitsRepository, EmployeeBenefitsRepository>(); // Make sure you have a PayrollRepository class that implements IPayrollRepository

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
