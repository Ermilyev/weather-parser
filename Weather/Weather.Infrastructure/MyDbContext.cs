using Microsoft.EntityFrameworkCore;
using Weather.Domain.Entities.Cities;
using Weather.Domain.Entities.Dates;
using Weather.Domain.Entities.Weathers;
using Weather.Infrastructure.Configurations;

namespace Weather.Infrastructure;

public sealed class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
{
    public DbSet<ForecastCity> Cities { get; set; }
    public DbSet<ForecastDate> Dates { get; set; }
    public DbSet<ForecastWeather> Weathers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ForecastCityConfiguration());
        modelBuilder.ApplyConfiguration(new ForecastDateConfiguration());
        modelBuilder.ApplyConfiguration(new ForecastWeatherConfiguration());
    }
}