using Common.Models.Entities;

namespace Common.Domain.Repositories;

public interface ICRUDRepository<TEntity> :
    IGetRepository<TEntity>,
    ICreateRepository<TEntity>,
    IUpdateRepository<TEntity>,
    IRemoveRepository<TEntity> where TEntity : IEntity { }