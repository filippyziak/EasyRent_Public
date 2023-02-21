namespace EasyRent.Shared.Pagination;

public record PaginationDecorator
{
    public int CurrentPage { get; init; }
    public int CurrentCount { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }

    public static PaginationDecorator FromPagedList<T>(IPagedList<T> source)
        => new()
        {
            CurrentPage = source.CurrentPage,
            CurrentCount = source.CurrentCount,
            PageSize = source.PageSize,
            TotalCount = source.TotalCount,
            TotalPages = source.TotalPages
        };
}