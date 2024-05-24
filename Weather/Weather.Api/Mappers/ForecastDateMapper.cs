using Common.Domain.Aggregates;
using Common.Domain.ValueObjects;
using Weather.Api.Builders.Dates;
using Weather.Api.Models.Dates;
using Weather.Domain.Entities.Dates;

namespace Weather.Api.Mappers;

public static class ForecastDateMapper
{
    public static IReadOnlyList<ForecastDateModel> AsModels(this IReadOnlyList<ForecastDate> dates)
    {
        var models = new List<ForecastDateModel>(dates.Count);
        models.AddRange(dates.Select(date => date.AsModel()));
        return models;
    }
    
    public static ForecastDateModel AsModel(this ForecastDate date)
    {
        return new ForecastDateModel
        {
            Id = date.Id,
            Day = date.Day
        };
    }
    
    public static ForecastDate AsDate(this CreateForecastDateModel create)
    {
        return new ForecastDate
        {
            Id = new EntityId(),
            Day = create.Day,
        };
    }
    
    public static FindEntities<ForecastDate> AsFind(this ForecastDatesFilter filter, PaginationRecord pagination)
    {
        var propertiesBuilder= new ForecastDatesFilterPropertiesBuilder(filter);
        return new FindEntities<ForecastDate>
        {
            Properties = propertiesBuilder.BuildProperties(),
            Pagination = pagination
        };
    }
    
    public static UpdateEntities<ForecastDate> AsUpdate(this UpdateForecastDateModel update)
    {
        var propertiesBuilder = new ForecastDatesUpdatePropertiesBuilder(update);
        return new UpdateEntities<ForecastDate>
        {
            Properties = propertiesBuilder.BuildProperties(),
        };
    }
    
    public static ForecastDate ApplyUpdates(this ForecastDate date, UpdateForecastDateModel model)
    {
        if (model.Day != null)
        {
            date.Day = model.Day.Value;
        }

        return date;
    }
}