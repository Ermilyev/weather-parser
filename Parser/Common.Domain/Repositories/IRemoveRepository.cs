using Common.Models.Entities;

namespace Common.Domain.Repositories;

public interface IRemoveRepository<in TEntity> where TEntity : IEntity
{
    public Task RemoveAsync(TEntity entity);
    public Task RemoveAsync(IEnumerable<TEntity> entities);
}