using System.Linq;
using AutoMapper;
using EasyRent.Shared.Extensions;
using EasyRent.Shared.Pagination;

namespace EasyRent.Infrastructure.Extensions;

public static class MapperExtensions
{
    public static IPagedList<TDestination> ToPagedList<TDestination, TSource>(this IMapper mapper, IPagedList<TSource> source)
        => source
            .Select(x => mapper.Map<TDestination>(x))
            .ToPagedList(source.CurrentPage, source.PageSize, source.TotalCount);
}