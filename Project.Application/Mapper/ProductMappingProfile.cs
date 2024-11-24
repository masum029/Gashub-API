using AutoMapper;
using Project.Application.DTOs;
using Project.Domail.Entities;


namespace Project.Application.Mapper
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
