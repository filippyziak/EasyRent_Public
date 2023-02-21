using System.Reflection;
using EasyRent.CQRS.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.CQRS;

public static class LibraryContainersRegistrator
{
    public static IServiceCollection AddMediatrWithBehaviors(this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services
            .AddMediatR(assemblies)
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));

        return services;
    }
}