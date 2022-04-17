using Common.Domain.Repositories;
using Common.Infrastructure.Utility.EF;
using Common.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Repositories;

public class CRUDRepository<TEntity> : ICRUDRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly EFContext Context;

    protected CRUDRepository(EFContext context)
    {
        Context = context;
    }

    public async Task InsertAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task InsertAsync(IEnumerable<TEntity> entities)
    {
        await Context.Set<TEntity>().AddRangeAsync(entities);
        await Context.SaveChangesAsync();
    }

    public async Task<TEntity?> GetOneAsync(Guid id)
    {
        IQueryable<TEntity?> query = Context.Set<TEntity>()
            .AsQueryable()
            .Where(item => item.Id == id);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TEntity>> GetManyAsync(IEnumerable<Guid> ids)
    {
        var query = Context.Set<TEntity>()
            .AsQueryable()
            .Where(item => ids.Contains(item.Id));
        return (await query.ToListAsync());
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        var query = Context.Set<TEntity>().AsQueryable();
        return await query.ToListAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().UpdateRange(entities);
        await Context.SaveChangesAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync();
    }
    
    public async Task RemoveAsync(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
        await Context.SaveChangesAsync();
    }
}