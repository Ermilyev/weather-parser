using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Client.Models.Weather;

public class WeatherSummary
{
    [Required][JsonProperty("weathers")]
    public WeatherModel[] WeatherModels { get; set; } = Array.Empty<WeatherModel>();
    [Required][JsonProperty("count")]
    public long Count { get; set; }
}