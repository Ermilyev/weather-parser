using Common.Domain.Aggregates;
using Common.Domain.ValueObjects;
using Weather.Api.Builders.Cities;
using Weather.Api.Models.Cities;
using Weather.Domain.Entities.Cities;

namespace Weather.Api.Mappers;

public static class ForecastCityMapper
{
    public static IReadOnlyList<ForecastCityModel> AsModels(this IReadOnlyList<ForecastCity> cities)
    {
        var models = new List<ForecastCityModel>(cities.Count);
        models.AddRange(cities.Select(city => city.AsModel()));
        return models;
    }
    
    public static ForecastCityModel AsModel(this ForecastCity city)
    {
        return new ForecastCityModel
        {
            Id = city.Id,
            Name = city.Name
        };
    }
    
    public static ForecastCity AsCity(this CreateForecastCityModel create)
    {
        return new ForecastCity
        {
            Id = new EntityId(),
            Name = create.Name,
        };
    }
    
    public static FindEntities<ForecastCity> AsFind(this ForecastCitiesFilter filter, PaginationRecord pagination)
    {
        var propertiesBuilder= new ForecastCitiesFilterPropertiesBuilder(filter);
        return new FindEntities<ForecastCity>
        {
            Properties = propertiesBuilder.BuildProperties(),
            Pagination = pagination
        };
    }
    
    public static UpdateEntities<ForecastCity> AsUpdate(this UpdateForecastCityModel update)
    {
        var propertiesBuilder = new ForecastCitiesUpdatePropertiesBuilder(update);
        return new UpdateEntities<ForecastCity>
        {
            Properties = propertiesBuilder.BuildProperties(),
        };
    }
    
    public static ForecastCity ApplyUpdates(this ForecastCity city, UpdateForecastCityModel model)
    {
        if (model.Name != null)
        {
            city.Name = model.Name;
        }

        return city;
    }
}