using System.Text;

namespace Client.Infrastructure;

public static class UrlAdaptor
{
    public static string GetCityLink(Guid[]? ids = null, string[]? names = null)
    {
        var sb = new StringBuilder("city");
        
        if (ids is not null)
            foreach (var id in ids)
                sb.Append($"&ids={id}");

        if (names is null) 
            return GetRelativeUri(sb.ToString());
        
        foreach (var name in names)
            sb.Append($"&names={name}");

        return sb.ToString().GetRelativeUri();
    }
    
    public static string GetWeatherLink(int skip, int? limit, Guid[]? ids = null, Guid[]? cityIds = null, Guid[]? forecastDateIds = null)
    {
        var sb = new StringBuilder("weather");
       
        if (skip > 0)
            sb.Append($"&skip={skip}");
        
        if (limit is not null)
            sb.Append($"&limit={limit}");
        
        if (ids is not null)
            foreach (var id in ids)
                sb.Append($"&ids={id}");

        if (cityIds is null) 
            return GetRelativeUri(sb.ToString());
        
        foreach (var cityId in cityIds)
            sb.Append($"&cityIds={cityId}");
        
        if (forecastDateIds is null) 
            return GetRelativeUri(sb.ToString());
        
        foreach (var forecastDateId in forecastDateIds)
            sb.Append($"&forecastDateIds={forecastDateId}");

        return sb.ToString().GetRelativeUri();
    }

    private static string GetRelativeUri(this string url)
    {
        var index = url.IndexOf("&", StringComparison.Ordinal);
        
        if (index < 0)
            return url;
        
        var ch = url.ToCharArray();
        ch[index] = '?';
        
        return  new string(ch) ?? url;
    }
}