using System.Linq.Expressions;
using Common.Domain.Entities;

namespace Common.Domain.Aggregates;

public sealed record UpdateEntities<TEntity>  where TEntity : class, IEntity
{
    public required IEnumerable<Expression<Func<TEntity, object>>> Properties { get; init; }
}