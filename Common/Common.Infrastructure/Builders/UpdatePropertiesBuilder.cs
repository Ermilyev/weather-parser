using System.Linq.Expressions;
using Common.Domain.Builders;
using Common.Domain.Entities;

namespace Common.Infrastructure.Builders;

public abstract class UpdatePropertiesBuilder<TEntity> : IUpdatePropertiesBuilder<TEntity> where TEntity : class, IEntity
{
    public abstract IEnumerable<Expression<Func<TEntity, object>>> BuildProperties();
}
