using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Parser.Models.ForecastDates;

public class ForecastDateSummary
{
    [Required]
    [JsonProperty("dates")]
    public ForecastDateModel[] ForecastDateModels { get; set; } = Array.Empty<ForecastDateModel>();
    [Required]
    [JsonProperty("count")]
    public long Count { get; set; }
}