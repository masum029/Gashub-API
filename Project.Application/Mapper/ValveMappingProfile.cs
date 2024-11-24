using AutoMapper;
using Project.Application.DTOs;
using Project.Domail.Entities;


namespace Project.Application.Mapper
{
    public class ValveMappingProfile : Profile
    {
        public ValveMappingProfile()
        {
            CreateMap<Valve, ValveDTO>().ReverseMap();
        }
    }
}
