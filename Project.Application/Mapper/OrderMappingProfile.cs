using AutoMapper;
using Project.Application.DTOs;
using Project.Domail.Entities;

namespace Project.Application.Mapper
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile() {
            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
