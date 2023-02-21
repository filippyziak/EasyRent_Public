using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyRent.Configuration;
using EasyRent.CQRS;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing.EventStore;
using EasyRent.Infrastructure;
using EasyRent.NetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.Libraries.Provider;

public static class LibraryContainersRegistrator
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        string serviceName,
        string version,
        IConfiguration configuration,
        IReadOnlyList<string> modulesAssembliesNames,
        IEnvironmentProvider environmentProvider)
        => services
            .AddAspNetCore(serviceName,
                version,
                configuration,
                modulesAssembliesNames,
                environmentProvider)
            .AddLogger()
            .AddDependencyInjectionProvider()
            .AddMediatrWithBehaviors(ConvertModuleAssembliesNamesToCore(modulesAssembliesNames))
            .AddMapperProfiles()
            .AddRetryPolicyRegistry()
            .AddDefaultRetryPolicy()
            .AddEventStore(configuration);

    public static IApplicationBuilder UseInfrastructure(this WebApplication app, IEnvironmentProvider environmentProvider)
        => app.UseAspNetCore(environmentProvider);

    private static Assembly[] ConvertModuleAssembliesNamesToCore(IEnumerable<string> moduleAssembliesNames)
        => moduleAssembliesNames
            .Select(assemblyName => Assembly.Load($"{assemblyName}.Core"))
            .ToArray();
}