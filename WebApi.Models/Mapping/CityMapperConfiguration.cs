using AutoMapper;
using Common.Models.Models;
using WebApi.Models.Cities;

namespace WebApi.Models.Mapping;

public class CityMapperConfiguration : Profile
{
    public CityMapperConfiguration()
    {
        CreateMap<City, CityModel>()
            .ForMember("Id", opt => opt.MapFrom(c => c.Id))
            .ForMember("Name", opt => opt.MapFrom(c => c.Name))
            .ReverseMap();
        
        CreateMap<City, CreateCityModel>()
            .ForMember("Name", opt => opt.MapFrom(c => c.Name))
            .ReverseMap();
        
        CreateMap<City, UpdateCityModel>()
            .ForMember("Name", opt => opt.MapFrom(c => c.Name))
            .ReverseMap();
    }
}