using System.Collections.Generic;
using System.Linq;
using EasyRent.Shared.Pagination;

namespace EasyRent.Shared.Extensions;

public static class EnumerableExtensions
{
    public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageNumber, int pageSize, int totalCount)
        => new PagedList<T>(source.ToList(), totalCount, pageNumber, pageSize);
}