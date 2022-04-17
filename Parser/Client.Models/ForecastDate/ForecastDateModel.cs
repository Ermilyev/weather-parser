using System.ComponentModel.DataAnnotations;

namespace Client.Models.ForecastDate;

public record ForecastDateModel
{
    public Guid Id { get; set; }
    [Required] public DateTime Date { get; set; }

    public void Deconstruct(out Guid forecastDateId, out DateTime forecastDateDate)
    {
        forecastDateId = Id;
        forecastDateDate = Date;
    }
}
