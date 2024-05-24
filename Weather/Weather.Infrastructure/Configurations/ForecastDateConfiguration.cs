using Common.Infrastructure.Utilities.EF.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Domain.Entities.Dates;

namespace Weather.Infrastructure.Configurations;

public sealed class ForecastDateConfiguration : EntityConfiguration<ForecastDate>
{
    public override void Configure(EntityTypeBuilder<ForecastDate> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Day)
            .IsRequired();
        
        builder.HasMany(e => e.Weather)
            .WithOne(w => w.Date)
            .HasForeignKey(w => w.DateId);
    }
}