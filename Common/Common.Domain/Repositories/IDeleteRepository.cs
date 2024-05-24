using Common.Domain.ValueObjects;

namespace Common.Domain.Repositories;

public interface IDeleteRepository
{
    public Task<bool> DeleteAsync(EntityId id, CancellationToken ct);
}