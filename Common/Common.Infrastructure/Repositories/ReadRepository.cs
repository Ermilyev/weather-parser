using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.Repositories;
using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Repositories;

public abstract class ReadRepository<TEntity>(DbContext dbContext) : IReadRepository<TEntity>
    where TEntity : class, IEntity
{
    private const int MaxCount = 25;
    public async Task<TEntity?> GetByIdAsync(EntityId id, CancellationToken ct)
    {
        return await dbContext
            .Set<TEntity>()
            .FindAsync([id], ct);
    }

    public async Task<(IReadOnlyList<TEntity> entities, int total)> GetByFilterAsync(FindEntities<TEntity> find, CancellationToken ct)
    {
        var query = dbContext.Set<TEntity>().Where(find.Properties);
        var total = await query.CountAsync(ct);
        var skip = find.Pagination.PageNumber;
        var limit = find.Pagination.PageSize ?? MaxCount;
        
        var entities = await query
            .Skip(skip * limit)
            .Take(limit)
            .ToListAsync(ct);

        return (entities, total);
    }

    public async Task<bool> ExistsAsync(EntityId id, CancellationToken ct)
    {
        return await dbContext.
            Set<TEntity>()
            .AnyAsync(e
                => EF.Property<EntityId>(e, "Id") == id, cancellationToken: ct);
    }

    public async Task<long> CountAsync(CancellationToken ct)
    {
        return await dbContext
            .Set<TEntity>()
            .CountAsync(cancellationToken: ct);
    }
}