using System.Linq.Expressions;
using Common.Domain.Entities;

namespace Common.Domain.Builders;

public interface IFilterPropertiesBuilder<TEntity> where TEntity : class, IEntity
{
    Expression<Func<TEntity, bool>> BuildProperties();
}