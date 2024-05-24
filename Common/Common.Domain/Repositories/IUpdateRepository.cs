using Common.Domain.Aggregates;
using Common.Domain.Entities;

namespace Common.Domain.Repositories;

public interface IUpdateRepository<TEntity>  where TEntity : class, IEntity
{
    public Task<TEntity> UpdateAsync(TEntity entity, UpdateEntities<TEntity> update, CancellationToken ct);
}