using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Infrastructure.Utilities.EF.Converters;

public sealed class EntityIdConverter() : ValueConverter<EntityId, Guid>(id => id.Value,
    value => new EntityId(value));