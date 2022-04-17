using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models.Cities;

public class CitySummaryInfoModel
{
    /// <summary>
    /// The array of cities
    /// </summary>
    [Required, JsonPropertyName("cities")] 
    public CityModel[] CityModelList { get; set; } = Array.Empty<CityModel>();
    /// <summary>
    /// The count of cities
    /// </summary>
    /// <example>135</example>
    [Required, JsonPropertyName("count")] 
    public long Count { get; set; }
}