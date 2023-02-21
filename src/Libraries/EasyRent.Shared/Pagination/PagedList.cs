using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRent.Shared.Pagination;

public class PagedList<T> : List<T>, IPagedList<T>
{
    public int CurrentPage { get; init; }
    public int CurrentCount { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);

        CurrentCount = Count;
    }

    private PagedList()
    {
    }

    public static PagedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    public static IPagedList<T> Empty => new PagedList<T>();
}