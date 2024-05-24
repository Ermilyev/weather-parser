using Common.Infrastructure.Utilities.EF.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Domain.Entities.Weathers;

namespace Weather.Infrastructure.Configurations;

public sealed class ForecastWeatherConfiguration : EntityConfiguration<ForecastWeather>
{
    public override void Configure(EntityTypeBuilder<ForecastWeather> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(e => e.City)
            .WithMany(c => c.Weather)
            .HasForeignKey(e => e.CityId)
            .IsRequired();

        builder.HasOne(e => e.Date)
            .WithMany(d => d.Weather)
            .HasForeignKey(e => e.DateId)
            .IsRequired();
    }
}
