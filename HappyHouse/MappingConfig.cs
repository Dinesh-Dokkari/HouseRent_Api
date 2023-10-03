using AutoMapper;

namespace HappyHouse.Models
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<HouseDto, HouseUpdateDto>().ReverseMap();
            CreateMap<HouseDto, HouseCreateDto>().ReverseMap();

            CreateMap<HouseNumberDto, HouseNumberUpdateDto>().ReverseMap();
            CreateMap<HouseNumberDto, HouseNumberCreateDto>().ReverseMap();
        }
    }
}
