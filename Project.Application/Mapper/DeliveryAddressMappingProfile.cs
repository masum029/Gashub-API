using AutoMapper;
using Project.Application.DTOs;
using Project.Domail.Entities;

namespace Project.Application.Mapper
{
    public class DeliveryAddressMappingProfile : Profile
    {
        public DeliveryAddressMappingProfile()
        {
            CreateMap<DeliveryAddress, DeliveryAddressDTO>().ReverseMap();
        }
    }
}
