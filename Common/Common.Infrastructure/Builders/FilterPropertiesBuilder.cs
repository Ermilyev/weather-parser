using System.Linq.Expressions;
using Common.Domain.Builders;
using Common.Domain.Entities;

namespace Common.Infrastructure.Builders;

public abstract class FilterPropertiesBuilder<TEntity> : IFilterPropertiesBuilder<TEntity> where TEntity : class, IEntity
{
    public abstract Expression<Func<TEntity, bool>> BuildProperties();
}