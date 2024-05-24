using Common.Domain.Entities;

namespace Common.Domain.Repositories;

public interface IWriteRepository<TEntity> :
    ICreateRepository<TEntity>,
    IUpdateRepository<TEntity>,
    IDeleteRepository
    where TEntity : class, IEntity { }