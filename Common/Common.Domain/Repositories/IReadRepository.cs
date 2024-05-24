using Common.Domain.Entities;

namespace Common.Domain.Repositories;

public interface IReadRepository<TEntity> :
    IGetRepository<TEntity>,
    IExistsRepository
    where TEntity : class, IEntity;