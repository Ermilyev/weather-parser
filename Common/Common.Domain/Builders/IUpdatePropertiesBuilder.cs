using System.Linq.Expressions;
using Common.Domain.Entities;

namespace Common.Domain.Builders;

public interface IUpdatePropertiesBuilder<TEntity> where TEntity : class, IEntity
{
    IEnumerable<Expression<Func<TEntity, object>>> BuildProperties();
}