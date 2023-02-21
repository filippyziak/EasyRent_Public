using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Shared.Pagination;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EasyRent.Infrastructure.DocumentStore;

public class MongoDbPagedList<T> : List<T>, IPagedList<T>
{
    public int CurrentPage { get; init; }
    public int CurrentCount { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }

    public MongoDbPagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);

        CurrentCount = Count;
    }

    private MongoDbPagedList()
    {
    }

    public static async Task<MongoDbPagedList<T>> CreateAsync(IMongoQueryable<T> source, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new MongoDbPagedList<T>(items, count, pageNumber, pageSize);
    }

    public static IPagedList<T> Empty => new MongoDbPagedList<T>();
}