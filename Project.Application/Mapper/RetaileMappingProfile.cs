using AutoMapper;
using Project.Application.DTOs;
using Project.Domail.Entities;

namespace Project.Application.Mapper
{
    public class RetaileMappingProfile : Profile
    {
        public RetaileMappingProfile() {
            CreateMap<Retailer, RetailerDTO>().ReverseMap();

        }
    }
}
