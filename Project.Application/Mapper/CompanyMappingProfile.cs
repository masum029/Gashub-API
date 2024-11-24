using AutoMapper;
using Project.Application.DTOs;
using Project.Domail.Entities;

namespace Project.Application.Mapper
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile() {
            CreateMap<Company, CompanyDTO>().ReverseMap();
        }
    }
}
