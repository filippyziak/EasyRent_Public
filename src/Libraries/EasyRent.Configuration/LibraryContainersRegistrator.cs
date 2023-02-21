using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.Configuration;

public static class LibraryContainersRegistrator
{
    public static IServiceCollection AddModuleConfigurationProvider<TConfiguration>(this IServiceCollection services, IConfiguration configuration)
        where TConfiguration : class
        => services
            .AddSingleton(typeof(IConfigurationProvider<TConfiguration>),
                typeof(ModuleConfigurationProvider<TConfiguration>))
            .Configure<TConfiguration>(configuration);
}