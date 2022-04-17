using Common.Models.Entities;

namespace Common.Domain.Repositories;

public interface ICreateRepository<in TEntity> where TEntity : IEntity
{
    public Task InsertAsync(TEntity entity);
    public Task InsertAsync(IEnumerable<TEntity> entities);
}