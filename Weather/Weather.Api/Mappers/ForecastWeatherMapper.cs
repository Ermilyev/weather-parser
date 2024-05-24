using Common.Domain.Aggregates;
using Common.Domain.ValueObjects;
using Weather.Api.Builders.Weathers;
using Weather.Api.Models.Weathers;
using Weather.Domain.Entities.Weathers;

namespace Weather.Api.Mappers;

public static class ForecastWeatherMapper
{
    public static IReadOnlyList<ForecastWeatherModel> AsModels(this IReadOnlyList<ForecastWeather> weathers)
    {
        var models = new List<ForecastWeatherModel>(weathers.Count);
        models.AddRange(weathers.Select(weather => weather.AsModel()));
        return models;
    }
    
    public static ForecastWeatherModel AsModel(this ForecastWeather weather)
    {
        return new ForecastWeatherModel
        {
            Id = weather.Id,
            CityId = weather.CityId,
            DateId = weather.DateId,
            Name = weather.Name
        };
    }
    
    public static ForecastWeather AsDate(this CreateForecastWeatherModel create)
    {
        return new ForecastWeather
        {
            Id = new EntityId(),
            CityId = create.CityId,
            DateId = create.DateId,
            Name = create.Name
        };
    }
    
    public static FindEntities<ForecastWeather> AsFind(this ForecastWeathersFilter filter, PaginationRecord pagination)
    {
        var propertiesBuilder= new ForecastWeathersFilterPropertiesBuilder(filter);
        return new FindEntities<ForecastWeather>
        {
            Properties = propertiesBuilder.BuildProperties(),
            Pagination = pagination
        };
    }
    
    public static UpdateEntities<ForecastWeather> AsUpdate(this UpdateForecastWeatherModel update)
    {
        var propertiesBuilder = new ForecastWeathersUpdatePropertiesBuilder(update);
        return new UpdateEntities<ForecastWeather>
        {
            Properties = propertiesBuilder.BuildProperties(),
        };
    }
    
    public static ForecastWeather ApplyUpdates(this ForecastWeather weather, UpdateForecastWeatherModel model)
    {
        if (model.CityId != null)
        {
            weather.CityId = model.CityId.Value;
        }
        
        if (model.DateId != null)
        {
            weather.DateId = model.DateId.Value;
        }
        
        if (model.Name != null)
        {
            weather.Name = model.Name;
        }

        return weather;
    }
}