using AutoMapper;
using HouseRent_Api.Models;
using HouseRent_Api.Models.DTO;

namespace HouseRent_Api
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<House, HouseUpdateDto>().ReverseMap();
            CreateMap<House, HouseCreateDto>().ReverseMap();
            CreateMap<House, HouseDto>().ReverseMap();

            CreateMap<HouseNumber, HouseNumberUpdateDto>().ReverseMap();
            CreateMap<HouseNumber, HouseNumberCreateDto>().ReverseMap();
            CreateMap<HouseNumber, HouseNumberDto>().ReverseMap();



            
        }
    }
}
