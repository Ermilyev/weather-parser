namespace Parser.Models.Weathers.Aggregates
{
    public class WeatherPartsSummary
    {
        public List<WeatherPart> Sections { get; set; } = new List<WeatherPart>();
        public int Count => Sections.Count;
    }
}
