using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models.Weathers;

public class WeatherSummaryInfoModel
{
    /// <summary>
    /// The array of weathers
    /// </summary>
    [Required, JsonPropertyName("weathers")]
    public WeatherModel[] WeatherModels { get; set; } = Array.Empty<WeatherModel>();
    /// <summary>
    /// The count of weathers
    /// </summary>
    /// <example>135</example>
    [Required, JsonPropertyName("count")] 
    public long Count { get; set; }
}