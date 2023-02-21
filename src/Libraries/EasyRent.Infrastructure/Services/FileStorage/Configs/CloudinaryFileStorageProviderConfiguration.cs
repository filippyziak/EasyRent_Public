namespace EasyRent.Infrastructure.Services.FileStorage.Configs;

public record CloudinaryFileStorageProviderConfiguration
{
    public string ApiKey { get; init; }
    public string ApiSecret { get; init; }
    public string CloudName { get; init; }
}