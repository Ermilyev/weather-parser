using Weather.Api.Services;
using Weather.Infrastructure;
using Weather.Infrastructure.Repositories;

namespace Weather.Api;

public static class DIExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        return services;
    }

    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services
            .AddForecastDateRepositories()
            .AddForecastCityRepositories()
            .AddForecastWeatherRepositories();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<ForecastDateService>()
            .AddScoped<ForecastCityService>()
            .AddScoped<ForecastWeatherService>();
        return services;
    }
}