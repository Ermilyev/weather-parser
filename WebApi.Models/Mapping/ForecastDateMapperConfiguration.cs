using AutoMapper;
using Common.Models.Models;
using WebApi.Models.ForecastDates;

namespace WebApi.Models.Mapping;

public class ForecastDateMapperConfiguration : Profile
{
    public ForecastDateMapperConfiguration()
    {
        CreateMap<ForecastDate, ForecastDateModel>()
            .ForMember("Id", opt => opt.MapFrom(c => c.Id))
            .ForMember("Date", opt => opt.MapFrom(c => c.Date))
            .ReverseMap();
        
        CreateMap<ForecastDate, CreateForecastDateModel>()
            .ForMember("Date", opt => opt.MapFrom(c => c.Date))
            .ReverseMap();
        
        CreateMap<ForecastDate, UpdateForecastDateModel>()
            .ForMember("Date", opt => opt.MapFrom(c => c.Date))
            .ReverseMap();
    }
}