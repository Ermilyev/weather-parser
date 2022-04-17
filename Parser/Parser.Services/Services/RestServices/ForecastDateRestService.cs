using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure.Utility.RestSharp.Store;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Parser.Models.ForecastDates;

namespace Parser.Services.Services.RestServices;

public class ForecastDateRestService
{
    private readonly Uri _api;
    private readonly ILogger<ForecastDateRestService> _logger;
    private readonly IRestRepository _restRepository;

    public ForecastDateRestService(IConfiguration configuration, ILogger<ForecastDateRestService> logger, IRestRepository restRepository)
    {
        _api = new Uri(string.Concat(configuration["API:Url"],configuration["API:Version"]));
        _logger = logger;
        _restRepository = restRepository;
    }

    public async Task<List<ForecastDateModel>> WorkAsync(IEnumerable<DateTime> dates)
    {
        try
        {
            _logger.LogInformation(
                "Starting | Service: {Service}, Method: {Method}, Time - {Datetime}",  
                nameof(ForecastDateRestService),
                nameof(WorkAsync),
                DateTime.Now);
            
            var models = new List<ForecastDateModel>();
            dates.ToList().ForEach(date => models.Add(new ForecastDateModel() {Id=Guid.Empty, Date =date }));

            if (models.Any() == false)
                return new List<ForecastDateModel>();

            var existingModels = await ExistAsync(models);

            if (existingModels.Any())
            {
                foreach (var model in models)
                {
                    var item = existingModels.FirstOrDefault(d => d.Date == model.Date);
                    
                    if (item is null)
                        continue;
                    
                    if (item.Id != Guid.Empty)
                        model.Id = item.Id;
                }
            }

            var createModels = (from model in models 
                where model.Id == Guid.Empty 
                select new CreateForecastDateModel(model.Date))
                .ToList();

            var resultModels = models.Where(d => d.Id != Guid.Empty).ToList();
            foreach (var createModel in createModels)
            {
                var insertedModel = await InsertAsync(createModel);
                if (insertedModel.Id != Guid.Empty)
                    resultModels.Add(insertedModel);
            }
            
            _logger.LogInformation(
                "Finished | Service: {Service}, Method: {Method}, Time - {Datetime}",  
                nameof(ForecastDateRestService),
                nameof(WorkAsync),
                DateTime.Now);

            return resultModels;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}",  
                ex.Message,
                nameof(ForecastDateRestService),
                nameof(WorkAsync));
            
            throw;
        }
    }
    
    #region ForecastDateExtensions Methods

    private async Task<List<ForecastDateModel>> ExistAsync(IReadOnlyCollection<ForecastDateModel> forecastDateList)
    {
        try
        {
            _logger.LogInformation(
                "Starting | Service: {Service}, Method: {Method}, Time - {Datetime}",  
                nameof(ForecastDateRestService),
                nameof(ExistAsync),
                DateTime.Now);
            
            var dates = forecastDateList.Select(d => d.Date).ToHashSet();
            return await GetAsync(dates);
            
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Model: {Model}",  
                ex.Message,
                nameof(ForecastDateRestService),
                nameof(ExistAsync),
                forecastDateList);
            
            throw;
        }
    }
    #endregion
    
    #region REST Methods
    private async Task<ForecastDateModel> InsertAsync(CreateForecastDateModel createForecastModel)
    {
        try
        {
            _logger.LogInformation(
                "Service: {Service}, Method: {Method}, Model: {Model}",    
                nameof(ForecastDateRestService),
                nameof(InsertAsync),
                createForecastModel);
            
            const string relativeUri = $"forecastdate";

            var resultModel = await _restRepository
                .Insert<ForecastDateModel, CreateForecastDateModel>(new Uri(_api, relativeUri), createForecastModel);

            return resultModel;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Model: {Model}",  
                ex.Message,
                nameof(ForecastDateRestService),
                nameof(InsertAsync),
                createForecastModel);
            
            throw;
        }
    }
    
    private async Task<List<ForecastDateModel>> GetAsync(HashSet<DateTime> dates)
    {
        try
        {
            _logger.LogInformation(
                "Service: {Service}, Method: {Method}, Date: {Date}",   
                nameof(ForecastDateRestService),
                nameof(GetAsync),
                dates);
            
            var relativeUri = new StringBuilder();
            
            foreach (var date in dates)
            {
                relativeUri.Append(string.IsNullOrEmpty(relativeUri.ToString())
                    ? $"forecastdate?Dates={date:yyyy'-'MM'-'dd'T'HH':'mm':'ss}"
                    : $"&Dates={date:yyyy'-'MM'-'dd'T'HH':'mm':'ss}");
            }
            
            var resultModel = await _restRepository.Get<ForecastDateSummary>(new Uri(_api, relativeUri.ToString()));
            return resultModel.ForecastDateModels.Any() ? resultModel.ForecastDateModels.ToList() : new List<ForecastDateModel>();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error: {Error}, Service: {Service}, Method: {Method}, Date: {Date}",
                ex.Message,
                nameof(ForecastDateRestService),
                nameof(GetAsync),
                dates);
            
            throw;
        }
    }
    #endregion
}