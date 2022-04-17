using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Parser.Models.ForecastDates;

public record CreateForecastDateModel(DateTime Date)
{
    [Required]
    [JsonProperty("date")]
    public DateTime Date { get; set; } = Date;
}