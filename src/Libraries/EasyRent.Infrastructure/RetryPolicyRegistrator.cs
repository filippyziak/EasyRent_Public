using EasyRent.Infrastructure.Abstractions.RetryPolicy;
using EasyRent.Infrastructure.RetryPolicies;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.Infrastructure;

public static class RetryPolicyRegistrator
{
    public static IServiceCollection AddRetryPolicyRegistry(this IServiceCollection services)
        => services.AddSingleton<IRetryPolicyRegistry, RetryPolicyRegistry>();

    public static IServiceCollection AddRetryPolicy<TRetryPolicy>(this IServiceCollection services)
        where TRetryPolicy : class, IRetryPolicy
        => services.AddSingleton<IRetryPolicy, TRetryPolicy>();

    public static IServiceCollection AddDefaultRetryPolicy(this IServiceCollection services)
        => services.AddSingleton<IDefaultRetryPolicy, DefaultPollyRetryPolicy>();
}