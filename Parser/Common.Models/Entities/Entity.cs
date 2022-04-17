namespace Common.Models.Entities;

public class Entity : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
}