using Common.Domain.ValueObjects;

namespace Common.Domain.Entities;

public abstract class Entity : IEntity
{
    public required EntityId Id { get; init; } = Guid.NewGuid();
}