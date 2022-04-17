using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Domain.Repositories.Store;
using Client.Models.City;
using Serilog;

namespace Client.Services.Controllers;

public class CityController
{
    private readonly ILogger _logger;
    private readonly ICityRepository _cityRepository;
    
    public CityController(ILogger logger,
        ICityRepository cityRepository)
    {
        _logger = logger;
        _cityRepository = cityRepository;
    }

    public async Task<CitySummary> GetByFilter(Guid[]? ids = null, string[]? names = null)
    {
        return await _cityRepository.GetByFilter(ids, names);
    }
    
    public async Task<CityModel> GetCity(Guid id)
    {
        return await _cityRepository.GetCity(id);
    }
}