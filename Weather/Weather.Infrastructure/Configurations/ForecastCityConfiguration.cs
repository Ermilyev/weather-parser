using Common.Infrastructure.Utilities.EF.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Domain.Entities.Cities;

namespace Weather.Infrastructure.Configurations;

public sealed class ForecastCityConfiguration : EntityConfiguration<ForecastCity>
{
    public override void Configure(EntityTypeBuilder<ForecastCity> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(e => e.Weather)
            .WithOne(w => w.City)
            .HasForeignKey(w => w.CityId);
    }
}