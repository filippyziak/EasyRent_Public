using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Shared.Pagination;
using Microsoft.EntityFrameworkCore;

namespace EasyRent.Infrastructure.Database;

public class EntityFrameworkPagedList<T>: List<T>, IPagedList<T>
{
    public int CurrentPage { get; init; }
    public int CurrentCount { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }

    public EntityFrameworkPagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);

        CurrentCount = Count;
    }

    private EntityFrameworkPagedList()
    {
    }

    public static async Task<EntityFrameworkPagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new EntityFrameworkPagedList<T>(items, count, pageNumber, pageSize);
    }

    public static IPagedList<T> Empty => new EntityFrameworkPagedList<T>();
}