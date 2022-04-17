using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Domain.Repositories.Store;
using Client.Models.ForecastDate;
using Serilog;

namespace Client.Services.Controllers;

public class ForecastDateController
{
    private readonly ILogger _logger;
    private readonly IForecastDateRepository _forecastDateRepository;
    
    public ForecastDateController(ILogger logger,
        IForecastDateRepository forecastDateRepository)
    {
        _logger = logger;
        _forecastDateRepository = forecastDateRepository;
    }
    
    public async Task<IReadOnlyList<ForecastDateModel>> GetByFilter(DateTime[]? dates)
    {
        return await _forecastDateRepository.GetByFilter(dates);
    }
    
    public async Task<ForecastDateModel> GetForecastDate(Guid id)
    {
        return await _forecastDateRepository.GetForecastDate(id);
    }
}