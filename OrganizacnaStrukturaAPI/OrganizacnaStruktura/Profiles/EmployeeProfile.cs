using AutoMapper;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Profiles
{
    public class EmployeeProfile : Profile
    {
        //Profil na mapovanie zamestnanca
        public EmployeeProfile()
        {
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<Employee,EmployeeUpdateDto>();
        }
        
    }
}