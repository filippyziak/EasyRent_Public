using System.Linq;
using AutoMapper;
using EasyRent.Infrastructure.Logging;
using EasyRent.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.Infrastructure;

public static class LibraryContainersRegistrator
{
    public static IServiceCollection AddLogger(this IServiceCollection services)
        => services.AddSingleton(_ => NLogFactory.GetLoggerFromConfiguration());
    
    public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        var mapperProfileTypes = AppDomainExtensions
            .GetApplicationTypes()
            .Where(t => t.IsSubclassOf(typeof(Profile)));

        return services.AddAutoMapper(mapperProfileTypes
            .Select(t => t.Assembly));
    }
}