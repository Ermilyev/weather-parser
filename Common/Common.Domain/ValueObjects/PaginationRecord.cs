namespace Common.Domain.ValueObjects;

public sealed record PaginationRecord(int PageNumber, int? PageSize)
{
    public int PageNumber { get; } = PageNumber;
    public int? PageSize { get; } = PageSize;
}