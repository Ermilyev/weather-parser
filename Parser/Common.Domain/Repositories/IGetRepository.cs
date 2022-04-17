using Common.Models.Entities;

namespace Common.Domain.Repositories;

public interface IGetRepository<TEntity> where TEntity : IEntity
{
    public Task<TEntity?> GetOneAsync(Guid id);
    public Task<IReadOnlyList<TEntity>> GetManyAsync(IEnumerable<Guid> ids);
    public Task<IReadOnlyList<TEntity>> GetAllAsync();
}