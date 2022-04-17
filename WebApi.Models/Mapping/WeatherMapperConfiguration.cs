using AutoMapper;
using Common.Models.Models;
using WebApi.Models.Weathers;

namespace WebApi.Models.Mapping;

public class WeatherMapperConfiguration : Profile
{
    public WeatherMapperConfiguration()
    {
        CreateMap<Weather, WeatherModel>()
            .ForMember("Id", opt => opt.MapFrom(c => c.Id))
            .ForMember("CityId", opt => opt.MapFrom(c => c.CityId))
            .ForMember("ForecastDateId", opt => opt.MapFrom(c => c.ForecastDateId))
            .ForMember("ParsedAt", opt => opt.MapFrom(c => c.ParsedAt))
            .ForMember("MinTempCelsius", opt => opt.MapFrom(c => c.MinTempCelsius))
            .ForMember("MinTempFahrenheit", opt => opt.MapFrom(c => c.MinTempFahrenheit))
            .ForMember("MaxTempCelsius", opt => opt.MapFrom(c => c.MaxTempCelsius))
            .ForMember("MaxTempFahrenheit", opt => opt.MapFrom(c => c.MaxTempFahrenheit))
            .ForMember("MaxWindSpeedMetersPerSecond", opt => opt.MapFrom(c => c.MaxWindSpeedMetersPerSecond))
            .ForMember("MaxWindSpeedMilesPerHour", opt => opt.MapFrom(c => c.MaxWindSpeedMilesPerHour))
            .ReverseMap();
        
        CreateMap<Weather, CreateWeatherModel>()
            .ForMember("CityId", opt => opt.MapFrom(c => c.CityId))
            .ForMember("ForecastDateId", opt => opt.MapFrom(c => c.ForecastDateId))
            .ForMember("ParsedAt", opt => opt.MapFrom(c => c.ParsedAt))
            .ForMember("MinTempCelsius", opt => opt.MapFrom(c => c.MinTempCelsius))
            .ForMember("MinTempFahrenheit", opt => opt.MapFrom(c => c.MinTempFahrenheit))
            .ForMember("MaxTempCelsius", opt => opt.MapFrom(c => c.MaxTempCelsius))
            .ForMember("MaxTempFahrenheit", opt => opt.MapFrom(c => c.MaxTempFahrenheit))
            .ForMember("MaxWindSpeedMetersPerSecond", opt => opt.MapFrom(c => c.MaxWindSpeedMetersPerSecond))
            .ForMember("MaxWindSpeedMilesPerHour", opt => opt.MapFrom(c => c.MaxWindSpeedMilesPerHour))
            .ReverseMap();
        
        CreateMap<Weather, UpdateWeatherModel>()
            .ForMember("CityId", opt => opt.MapFrom(c => c.CityId))
            .ForMember("ForecastDateId", opt => opt.MapFrom(c => c.ForecastDateId))
            .ForMember("ParsedAt", opt => opt.MapFrom(c => c.ParsedAt))
            .ForMember("MinTempCelsius", opt => opt.MapFrom(c => c.MinTempCelsius))
            .ForMember("MinTempFahrenheit", opt => opt.MapFrom(c => c.MinTempFahrenheit))
            .ForMember("MaxTempCelsius", opt => opt.MapFrom(c => c.MaxTempCelsius))
            .ForMember("MaxTempFahrenheit", opt => opt.MapFrom(c => c.MaxTempFahrenheit))
            .ForMember("MaxWindSpeedMetersPerSecond", opt => opt.MapFrom(c => c.MaxWindSpeedMetersPerSecond))
            .ForMember("MaxWindSpeedMilesPerHour", opt => opt.MapFrom(c => c.MaxWindSpeedMilesPerHour))
            .ReverseMap();
    }
}