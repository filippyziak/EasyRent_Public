using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.DependencyInjection;

public static class LibraryContainersRegistrator
{
    public static IServiceCollection AddDependencyInjectionProvider(this IServiceCollection services)
        => services.AddSingleton<IDIProvider, DIProvider>();
}