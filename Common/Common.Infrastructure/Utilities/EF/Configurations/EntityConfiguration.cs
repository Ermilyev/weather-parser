using Common.Domain.Entities;
using Common.Domain.ValueObjects;
using Common.Infrastructure.Utilities.EF.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Infrastructure.Utilities.EF.Configurations;

public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
{
    private ValueConverter<EntityId, Guid> EntityIdConverter { get; } = new EntityIdConverter();

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasConversion(EntityIdConverter)
            .ValueGeneratedNever();
    }
}
