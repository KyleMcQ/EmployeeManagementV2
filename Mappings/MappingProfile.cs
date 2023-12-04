using MoviesAPI.DTOs;
using MoviesAPI.Models;
using AutoMapper;

namespace MoviesAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            // Add other mappings here
        }
    }
}
