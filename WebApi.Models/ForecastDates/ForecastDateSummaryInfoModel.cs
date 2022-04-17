using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models.ForecastDates;

public class ForecastDateSummaryInfoModel
{
    /// <summary>
    /// The array of dates
    /// </summary>
    [Required, JsonPropertyName("dates")]
    public ForecastDateModel[] ForecastDateModels { get; set; } = Array.Empty<ForecastDateModel>();
    /// <summary>
    /// The count of dates
    /// </summary>
    /// <example>135</example>
    [Required, JsonPropertyName("count")] 
    public long Count { get; set; }
}