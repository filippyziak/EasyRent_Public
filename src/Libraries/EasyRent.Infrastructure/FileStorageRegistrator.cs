using EasyRent.Infrastructure.Abstractions.Services.FileStorage;
using EasyRent.Infrastructure.Services.FileStorage;
using EasyRent.Infrastructure.Services.FileStorage.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.Infrastructure;

public static class FileStorageRegistrator
{
    public static IServiceCollection AddCloudinaryFileStorageSertvice(this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddSingleton<IFileStorageService, ColudinaryStorageService>()
            .Configure<CloudinaryFileStorageProviderConfiguration>(configuration.GetSection(nameof(CloudinaryFileStorageProviderConfiguration)));
}