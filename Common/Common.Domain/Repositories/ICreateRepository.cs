using Common.Domain.Entities;

namespace Common.Domain.Repositories;

public interface ICreateRepository<in TEntity>  where TEntity : class, IEntity
{
    public Task InsertAsync(TEntity entity, CancellationToken ct);
}