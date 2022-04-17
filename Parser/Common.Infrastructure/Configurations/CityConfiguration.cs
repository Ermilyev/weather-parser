using Common.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities")
            .HasKey(c => c.Id);
        builder.HasIndex(c => c.Name)
            .IsUnique();
        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .HasDefaultValue(string.Empty)
            .IsRequired();
        builder.HasMany(p => p.Weathers)
           .WithOne(t => t.City)
           .HasForeignKey(p => p.Id);
    }
}