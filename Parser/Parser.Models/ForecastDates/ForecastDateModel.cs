using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Parser.Models.ForecastDates;

public record ForecastDateModel()
{
    [Required]
    [JsonProperty("id")]
    public Guid Id { get; set; } = Guid.Empty;
    [Required]
    [JsonProperty("date")]
    public DateTime Date { get; set; } = DateTime.MinValue;
}
