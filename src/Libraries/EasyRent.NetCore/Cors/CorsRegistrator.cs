using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.NetCore.Cors;

public static class CorsRegistrator
{
    public static IServiceCollection AddCorsFromConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var corsConfiguration = configuration
            .GetSection(nameof(CorsConfiguration))
            .Get<CorsConfiguration>() ?? new CorsConfiguration();

        return services.AddCors(options =>
        {
            foreach (var policyName in corsConfiguration.OriginsPolicies.Keys)
                options.AddPolicy(policyName, builder =>
                {
                    builder
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(corsConfiguration.OriginsPolicies[policyName]);
                });
        });
    }
}