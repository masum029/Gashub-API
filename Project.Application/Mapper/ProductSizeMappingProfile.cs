using AutoMapper;
using Project.Application.DTOs;
using Project.Domail.Entities;

namespace Project.Application.Mapper
{
    public class ProductSizeMappingProfile : Profile
    {
        public ProductSizeMappingProfile() {
            CreateMap<ProductSize, ProductSizeDTO>().ReverseMap();
        }
    }
}
