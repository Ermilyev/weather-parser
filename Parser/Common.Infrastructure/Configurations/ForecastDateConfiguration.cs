using Common.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Configurations;

public class ForecastDateConfiguration : IEntityTypeConfiguration<ForecastDate>
{
    public void Configure(EntityTypeBuilder<ForecastDate> builder)
    {
        builder.ToTable("ForecastDates")
            .HasKey(f => f.Id);
        builder.HasIndex(f => f.Date)
            .IsUnique();
        builder.Property(f => f.Date)
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.Today)
            .IsRequired();
        builder.HasMany(p => p.Weathers)
           .WithOne(t => t.ForecastDate)
           .HasForeignKey(p => p.Id);
    }
}