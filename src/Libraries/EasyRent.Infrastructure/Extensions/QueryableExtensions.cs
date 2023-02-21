using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Database;
using EasyRent.Shared.Pagination;

namespace EasyRent.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> predicate, bool condition)
        => condition
            ? queryable.Where(predicate)
            : queryable;

    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> queryable, PaginationQuery paginationQuery,
        CancellationToken cancellationToken = default)
        => await EntityFrameworkPagedList<T>.CreateAsync(queryable, paginationQuery.PageNumber, paginationQuery.PageSize, cancellationToken);
}