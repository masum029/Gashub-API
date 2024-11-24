using AutoMapper;
using Project.Application.DTOs;
using Project.Domail.Entities;

namespace Project.Application.Mapper
{
    public class TraderMappingProfile : Profile
    {
        public TraderMappingProfile()
        {
            CreateMap<Trader, TraderDTO>().ReverseMap();
        }
    }
}
