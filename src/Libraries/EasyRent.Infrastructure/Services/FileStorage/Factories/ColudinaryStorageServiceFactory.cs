using System.Text;
using CloudinaryDotNet;
using EasyRent.Infrastructure.Services.FileStorage.Configs;

namespace EasyRent.Infrastructure.Services.FileStorage.Factories;

public static class ColudinaryStorageServiceFactory
{
    public static Cloudinary CreateCloudinaryClient(CloudinaryFileStorageProviderConfiguration configuration)
    {
        var cloudinaryUrl = new StringBuilder("cloudinary://")
            .Append(configuration.ApiKey)
            .Append(':')
            .Append(configuration.ApiSecret)
            .Append('@')
            .Append(configuration.CloudName)
            .ToString();

        return new Cloudinary(cloudinaryUrl);
    } 
}