using AutoMapper;
using Project.Application.DTOs;
using Project.Domail.Entities;


namespace Project.Application.Mapper
{
    internal class ProductDiscountMappingProfile : Profile
    {
        public ProductDiscountMappingProfile()
        {
            CreateMap<ProductDiscunt, ProductDiscuntDTO>().ReverseMap();
        }
    }
}
