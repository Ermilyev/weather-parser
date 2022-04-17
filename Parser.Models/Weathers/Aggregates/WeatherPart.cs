namespace Parser.Models.Weathers.Aggregates
{
    public record WeatherPart(Guid Id, Guid CityId, Guid ForecastDateId)
    {
        public Guid Id { get; set; } = Id;
        public Guid CityId { get; set; } = CityId;
        public Guid ForecastDateId { get; set; } = ForecastDateId;
    }
}
