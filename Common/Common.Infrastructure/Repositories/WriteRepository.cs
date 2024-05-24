using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.Repositories;
using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Repositories;

public abstract class WriteRepository<TEntity>(DbContext dbContext) : IWriteRepository<TEntity>
    where TEntity : class, IEntity
{
    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
    { 
        await dbContext.Set<TEntity>()
            .AddAsync(entity, cancellationToken);
        
        await dbContext
            .SaveChangesAsync(cancellationToken);
    }
    
    public async Task<TEntity> UpdateAsync(TEntity entity, UpdateEntities<TEntity> update, CancellationToken cancellationToken)
    {
        dbContext
            .Set<TEntity>()
            .Attach(entity);

        foreach (var property in update.Properties)
        {
            var entry = dbContext.Entry(entity).Property(property);
            entry.IsModified = true;
            Console.WriteLine($"Property '{property}' marked as modified. Current value: {entry.CurrentValue}");
            if (entry.IsModified)
            {
                Console.WriteLine($"Property '{property}' is marked as modified. Current value: {entry.CurrentValue}, Original value: {entry.OriginalValue}");
            }
        }
        
        var contextState = dbContext.ChangeTracker.DebugView.ShortView;
        Console.WriteLine($"Context State before SaveChanges: {contextState}");
        
        var result = await dbContext.SaveChangesAsync(cancellationToken);
        Console.WriteLine($"{result} changes saved");
    
        return entity;
    }

    public async Task<bool> DeleteAsync(EntityId id, CancellationToken cancellationToken)
    {
        var entity = await dbContext
            .Set<TEntity>()
            .FindAsync([id], cancellationToken: cancellationToken);
        
        if (entity == null)
        {
            return false;
        }

        dbContext
            .Set<TEntity>()
            .Remove(entity);
        
        await dbContext.
            SaveChangesAsync(cancellationToken);
        
        return true;
    }
}
