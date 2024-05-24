using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.ValueObjects;

namespace Common.Domain.Repositories;

public interface IGetRepository<TEntity> where TEntity : class, IEntity
{
    Task<(IReadOnlyList<TEntity> entities, int total)> GetByFilterAsync(FindEntities<TEntity> find, CancellationToken ct);
    Task<TEntity?> GetByIdAsync(EntityId id, CancellationToken ct);
}