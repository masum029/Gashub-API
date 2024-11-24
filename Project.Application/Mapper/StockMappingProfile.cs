using AutoMapper;
using Project.Application.DTOs;
using Project.Domail.Entities;

namespace Project.Application.Mapper
{
    public class StockMappingProfile : Profile
    {
        public StockMappingProfile()
        {
            CreateMap<Stock, StockDTO>().ReverseMap();
        }
    }
}
