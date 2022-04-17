using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parser.Models.Weathers;
using Parser.Models.Weathers.Aggregates;

namespace Parser.Services;

public static class Extension
{
    private const int Limit = 10;

    public static async Task<List<WeatherPartsSummary>> Sliced(
        this IEnumerable<WeatherPart> weatherParts)
    {
        var segmentsCount = weatherParts.ToHashSet().Count / Limit;
        var result = new List<WeatherPartsSummary>();

        await Task.Run(() =>
        {
            for (var i = 0; i < segmentsCount; i++)
            {
                var sections = weatherParts.ToHashSet().Skip(Limit * i).Take(Limit).ToList();
                result.Add(new WeatherPartsSummary() { Sections = sections });
            }
        });

        return result;
    }

    public static IEnumerable<WeatherModel> CopyTo(this IEnumerable<WeatherModel> source, IEnumerable<WeatherModel> destination)
    {
        foreach (var model in source)
        {
            var item = destination.FirstOrDefault(d => d.CityId == model.CityId && d.ForecastDateId == model.ForecastDateId);

            if (item == null)
                continue;

            if (item.Id != Guid.Empty)
                model.Id = item.Id;
        }

        return source;
    }
}