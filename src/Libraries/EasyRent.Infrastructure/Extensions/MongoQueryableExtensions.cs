using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.DocumentStore;
using EasyRent.Shared.Pagination;
using MongoDB.Driver.Linq;

namespace EasyRent.Infrastructure.Extensions;

public static class MongoQueryableExtensions
{
    public static IMongoQueryable<T> WhereIf<T>(this IMongoQueryable<T> queryable, Expression<Func<T, bool>> predicate, bool condition)
        => condition
            ? queryable.Where(predicate)
            : queryable;

    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IMongoQueryable<T> queryable, PaginationQuery paginationQuery,
        CancellationToken cancellationToken = default)
        => await MongoDbPagedList<T>.CreateAsync(queryable, paginationQuery.PageNumber, paginationQuery.PageSize, cancellationToken);
}