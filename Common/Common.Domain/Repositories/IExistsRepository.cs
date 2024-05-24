using Common.Domain.ValueObjects;

namespace Common.Domain.Repositories;

public interface IExistsRepository
{
    Task<bool> ExistsAsync(EntityId id, CancellationToken ct);
}