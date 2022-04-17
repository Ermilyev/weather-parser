using HtmlAgilityPack;
using Parser.Domain.Repositories;
using Parser.Models.Cities;

namespace Parser.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    public async Task<List<CityToParse>> ParseAsync(string url)
    {
        var cityList = await ParseCitiesEntryPointsAsync(url);

        if (cityList is not {Count: > 0})
            return cityList;

        foreach (var city in cityList)
            city.Link = city.TenDaysLink();
            
        return cityList;
    }

    private static async Task<List<CityToParse>> ParseCitiesEntryPointsAsync(string url)
    {
        var cityList = new List<CityToParse>();
        await Task.Run(() =>
        {
            var htmlWeb = new HtmlWeb();
            var htmlDocument = htmlWeb.Load(url);

            if (htmlDocument is null) return cityList;
            var cityLinkNodes = htmlDocument.DocumentNode
                .SelectNodes("//div[@class='cities-popular']/div[2]/div/a");

            cityList = (from cityLinkNode in cityLinkNodes
                let id = Guid.Empty
                let name = cityLinkNode.InnerText
                let path = cityLinkNode.GetAttributeValue("href", "")
                let cityModel = new CityToParse(name, path)
                where !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(path)
                select cityModel).ToList();

            return cityList;
        });
        
        return cityList;
    }
}