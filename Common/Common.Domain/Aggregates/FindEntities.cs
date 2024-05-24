using System.Linq.Expressions;
using Common.Domain.Entities;
using Common.Domain.ValueObjects;

namespace Common.Domain.Aggregates;

public sealed record FindEntities<TEntity> where TEntity : class, IEntity
{
    public required Expression<Func<TEntity, bool>> Properties { get; init; }
    public required PaginationRecord Pagination { get; init; }
}