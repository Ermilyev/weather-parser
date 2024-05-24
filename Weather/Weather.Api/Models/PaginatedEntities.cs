using System.ComponentModel.DataAnnotations;

namespace Weather.Api.Models;

public abstract class PaginatedEntities<TEntity>
{
    [Required]
    public required IReadOnlyList<TEntity> Items { get; set; }
    
    [Required, Range(0, int.MaxValue)]
    public required int Total { get; set; }

    public static PaginatedEntities<TEntity> Empty()
    {
        return new EmptyPaginatedEntities<TEntity>
        {
            Items = Array.Empty<TEntity>(),
            Total = 0
        };
    }

    private sealed class EmptyPaginatedEntities<T> : PaginatedEntities<T> { }
}


