using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Parser.Domain.Repositories;
using Parser.Models.Weathers;

namespace Parser.Services.Services;

internal class ParserService
{
    private readonly string _link;
    private readonly ILogger<ParserService> _logger;
    
    private readonly ICityRepository _cityRepository;
    private readonly IWeatherRepository _weatherRepository;
    
    public ParserService(IConfiguration configuration, ILogger<ParserService> logger, ICityRepository cityRepository,
                         IWeatherRepository weatherRepository)
    {
        _link = configuration["Parsing:Url"] ?? string.Empty;
        _logger = logger;
        _cityRepository = cityRepository;
        _weatherRepository = weatherRepository;
    }

    internal async Task<List<WeatherTenDaysModel>> ParseAsync()
    {
        try
        {
            _logger.LogInformation(
                "Starting | Service: {Service}, Method: {Method}, Time - {Datetime}",  
                nameof(ParserService),
                nameof(ParseAsync),
                DateTime.Now);
            
            var weatherItemList = new List<WeatherTenDaysModel>(); 

            var cityList =  await _cityRepository.ParseAsync(_link);;
            
            _logger.LogInformation(
                "Processed | Service: {Service}, Method: {Method}, Time - {Datetime}, Count: Cities - {Cities}",
                nameof(ParserService),
                nameof(ParseAsync),
                DateTime.Now,
                cityList.Count);
        
            if (cityList is not {Count: > 0})
                return weatherItemList;
            
            weatherItemList = await _weatherRepository.ParseAsync(cityList);

            _logger.LogInformation(
                "Finished | Service: {Service}, Method: {Method}, Time - {Datetime}, Count: Cities - {Cities}, Weathers - {Weathers}",
                nameof(ParserService),
                nameof(ParseAsync),
                DateTime.Now,
                cityList.Count,
                weatherItemList.Count);
            
            return weatherItemList;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}",  
                ex.Message,
                nameof(ParserService),
                nameof(ParseAsync));
            
            throw;
        }
    }
}