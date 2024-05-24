using System.ComponentModel.DataAnnotations;

namespace Weather.Api.Models.Dates;

public sealed class CreateForecastDateModel(DateTime day)
{
    [Required] 
    public required DateTime Day { get; init; } = day;
}