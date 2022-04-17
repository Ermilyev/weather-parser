using Common.Infrastructure.Utility.EF;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Repositories;
using WebApi.Infrastructure.Repositories;
using WebApi.Services.Services;
using Serilog;
using WebApi.Models.Mapping;

namespace WebApi.Services;

internal static class DIExtensions
{
    internal static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddScoped<ICityRepository, CityRepository>()
            .AddScoped<IForecastDateRepository, ForecastDateRepository>()
            .AddScoped<IWeatherRepository, WeatherRepository>();
    
    internal static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddScoped<CityService>()
            .AddScoped<ForecastDateService>()
            .AddScoped<WeatherService>();

    internal static IServiceCollection AddMapperProfiles(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddAutoMapper(typeof(CityMapperConfiguration), 
                typeof(ForecastDateMapperConfiguration), 
                typeof(WeatherMapperConfiguration));

    internal static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddDbContext<EFContext>()
            .AddScoped<DbContext>(sp => sp.GetRequiredService<EFContext>());

    internal static IServiceCollection AddLogger(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton(Log.Logger);
}