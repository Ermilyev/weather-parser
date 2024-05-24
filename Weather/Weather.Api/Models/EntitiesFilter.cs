namespace Weather.Api.Models;

public abstract class EntitiesFilter
{
    public HashSet<Guid>? Ids  { get; init; }
}