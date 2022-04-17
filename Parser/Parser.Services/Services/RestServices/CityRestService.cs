using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure.Utility.RestSharp.Store;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Parser.Models.Cities;

namespace Parser.Services.Services.RestServices;

public class CityRestService
{
    private readonly Uri _api;
    private readonly ILogger<CityRestService> _logger;
    private readonly IRestRepository _restRepository;

    public CityRestService(IConfiguration configuration, ILogger<CityRestService> logger, IRestRepository restRepository)
    {
        _api = new Uri(string.Concat(configuration["API:Url"],configuration["API:Version"]));
        _logger = logger;
        _restRepository = restRepository;
    }

    public async Task<List<CityModel>> WorkAsync(IEnumerable<string> cityNames)
    {
        try
        {
            _logger.LogInformation(
                "Starting | Service: {Service}, Method: {Method}, Time - {Datetime}",  
                nameof(CityRestService),
                nameof(WorkAsync),
                DateTime.Now);
            
            var models = new List<CityModel>();
            cityNames.ToList().ForEach(name => models.Add(new CityModel() {Id=Guid.Empty, Name = name }));

            if (models.Any() == false)
                return new List<CityModel>();;

            var existingModels = await ExistAsync(models);
            
            if (existingModels.Any())
            {
                foreach (var model in models)
                {
                    var item = existingModels.FirstOrDefault(d => d.Name == model.Name);
                    
                    if (item is null)
                        continue;
                    
                    if (item.Id != Guid.Empty)
                        model.Id = item.Id;
                }
            }

            var createModels = (from model in models 
                    where model.Id == Guid.Empty 
                    select new CreateCityModel(model.Name))
                .ToList();

            var resultModels = models.Where(c => c.Id != Guid.Empty).ToList();
            foreach (var createModel in createModels)
            {
                var insertedModel = await InsertAsync(createModel);
                if (insertedModel.Id != Guid.Empty)
                    resultModels.Add(insertedModel);
            }
            
            _logger.LogInformation(
                "Finished | Service: {Service}, Method: {Method}, Time - {Datetime}",  
                nameof(CityRestService),
                nameof(WorkAsync),
                DateTime.Now);

            return resultModels;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}",  
                ex.Message,
                nameof(CityRestService),
                nameof(WorkAsync));
            
            throw;
        }
    }
    
    #region CityExtensions Methods
    private async Task<List<CityModel>> ExistAsync(IReadOnlyCollection<CityModel> cityList)
    {
        try
        {
            _logger.LogInformation(
                "Starting | Service: {Service}, Method: {Method}, Time - {Datetime}",  
                nameof(CityRestService),
                nameof(ExistAsync),
                DateTime.Now);
            
            var cityNames = cityList.Select(c => c.Name).ToHashSet();
            return await GetAsync(cityNames);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Model: {Model}",  
                ex.Message,
                nameof(CityRestService),
                nameof(ExistAsync),
                cityList);
            
            throw;
        }
    }
    #endregion
    
    #region REST Methods
    private async Task<CityModel> InsertAsync(CreateCityModel createCityModel)
    {
        try
        {
            _logger.LogInformation(
                "Service: {Service}, Method: {Method}, Model: {Model}",    
                nameof(CityRestService),
                nameof(InsertAsync),
                createCityModel);
            
            const string relativeUri = $"city";

            var resultModel = await _restRepository.Insert<CityModel, CreateCityModel>(new Uri(_api, relativeUri), createCityModel);

            return resultModel;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Model: {Model}",  
                ex.Message,
                nameof(CityRestService),
                nameof(InsertAsync),
                createCityModel);
            
            throw;
        }
    }
    
    private async Task<List<CityModel>> GetAsync(HashSet<string> cityNames)
    {
        try
        {
            _logger.LogInformation(
                "Service: {Service}, Method: {Method}, Name: {Name}",
                nameof(CityRestService),
                nameof(GetAsync),
                cityNames);
            
            var relativeUri = new StringBuilder();

            foreach (var cityName in cityNames)
            {
                relativeUri.Append(string.IsNullOrEmpty(relativeUri.ToString())
                    ? $"city?Names={cityName}"
                    : $"&Names={cityName}");
            }

            var resultModel = await _restRepository.Get<CitySummary>(new Uri(_api, relativeUri.ToString()));
            return resultModel.CityModelList.Any() ? resultModel.CityModelList.ToList() : new List<CityModel>();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Name: {Name}",  
                ex.Message,
                nameof(CityRestService),
                nameof(GetAsync),
                cityNames);
            
            throw;
        }
    }
    #endregion
}