using Common.Infrastructure.Utility.RestSharp;
using Common.Infrastructure.Utility.RestSharp.Store;
using Microsoft.Extensions.DependencyInjection;
using Parser.Domain.Repositories;
using Parser.Infrastructure.Repositories;
using Parser.Models.Mapping;
using Parser.Services.Services;
using Parser.Services.Services.RestServices;

namespace Parser.Services;

internal static class DIExtensions
{
    internal static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddScoped<IWeatherRepository, WeatherRepository>()
            .AddScoped<ICityRepository, CityRepository>()
            .AddScoped<IRestRepository, RestRepository>();

    internal static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddHostedService<Worker>()
            .AddScoped<ParserService>()
            .AddScoped<RestService>()
            .AddScoped<CityRestService>()
            .AddScoped<ForecastDateRestService>()
            .AddScoped<WeatherRestService>();

    internal static IServiceCollection AddMapperProfiles(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddAutoMapper(typeof(WeatherMapperConfiguration));
}