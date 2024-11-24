using AutoMapper;
using Project.Application.DTOs;
using Project.Domail.Entities;

namespace Project.Application.Mapper
{
    public class ProdReturnMappingProfile : Profile
    {
        public ProdReturnMappingProfile() {
            CreateMap<ProdReturn, ProdReturnDTO>().ReverseMap();
        }
    }
}
