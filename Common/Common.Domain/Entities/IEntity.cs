using Common.Domain.ValueObjects;

namespace Common.Domain.Entities;

public interface IEntity
{
    EntityId Id { get; }
}