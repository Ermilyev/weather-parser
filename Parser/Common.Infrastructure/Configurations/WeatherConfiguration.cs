using Common.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Configurations;

public class WeatherConfiguration : IEntityTypeConfiguration<Weather>
{
    public void Configure(EntityTypeBuilder<Weather> builder)
    {
        builder.ToTable("Weathers")
            .HasKey(w => w.Id);
        builder.HasIndex(w => new{ w.CityId, w.ForecastDateId})
            .IsUnique();
        builder.Property(w => w.ParsedAt)
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.Today)
            .IsRequired();
        builder.Property(w => w.MinTempCelsius)
            .HasColumnName("MinTC")
            .HasDefaultValue(0)
            .IsRequired();
        builder.Property(w => w.MinTempFahrenheit)
            .HasColumnName("MinTF")
            .HasDefaultValue(0)
            .IsRequired();
        builder.Property(w => w.MaxTempCelsius)
            .HasColumnName("MaxTC")
            .HasDefaultValue(0)
            .IsRequired();
        builder.Property(w => w.MaxTempFahrenheit)
            .HasColumnName("MaxTF")
            .HasDefaultValue(0)
            .IsRequired();
        builder.Property(w => w.MaxWindSpeedMetersPerSecond)
            .HasColumnName("MaxSPS")
            .HasDefaultValue(0)
            .IsRequired();
        builder.Property(w => w.MaxWindSpeedMilesPerHour)
            .HasColumnName("MaxSPH")
            .HasDefaultValue(0)
            .IsRequired();
        builder.HasOne(p => p.City)
            .WithMany(t => t.Weathers)
            .HasForeignKey(p => p.CityId);
        builder.HasOne(p => p.ForecastDate)
            .WithMany(t => t.Weathers)
            .HasForeignKey(p => p.ForecastDateId);
    }
}