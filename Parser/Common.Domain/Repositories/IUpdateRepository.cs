using Common.Models.Entities;

namespace Common.Domain.Repositories;

public interface IUpdateRepository<in TEntity> where TEntity : IEntity
{
    public Task UpdateAsync(TEntity entity);
    public Task UpdateAsync(IEnumerable<TEntity> entities);
}