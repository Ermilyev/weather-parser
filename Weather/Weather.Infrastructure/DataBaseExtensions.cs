using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Weather.Infrastructure;

public static class DataBaseExtensions
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.
            AddDbContext<MyDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}