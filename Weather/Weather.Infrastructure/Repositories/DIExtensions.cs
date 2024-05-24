using Microsoft.Extensions.DependencyInjection;
using Weather.Domain.Repositories.Cities;
using Weather.Domain.Repositories.Dates;
using Weather.Domain.Repositories.Weathers;
using Weather.Infrastructure.Repositories.Cities;
using Weather.Infrastructure.Repositories.Dates;
using Weather.Infrastructure.Repositories.Weathers;

namespace Weather.Infrastructure.Repositories;

public static class DIExtensions
{
    public static IServiceCollection AddForecastDateRepositories(this IServiceCollection services)
    {
        services.AddScoped<IForecastDateReadRepository, ForecastDateReadRepository>()
                .AddScoped<IForecastDateWriteRepository, ForecastDateWriteRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddForecastCityRepositories(this IServiceCollection services)
    {
        services.AddScoped<IForecastCityReadRepository, ForecastCityReadRepository>()
            .AddScoped<IForecastCityWriteRepository, ForecastCityWriteRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddForecastWeatherRepositories(this IServiceCollection services)
    {
        services.AddScoped<IForecastWeatherReadRepository, ForecastWeatherReadRepository>()
            .AddScoped<IForecastWeatherWriteRepository, ForecastWeatherWriteRepository>();
        
        return services;
    }
}