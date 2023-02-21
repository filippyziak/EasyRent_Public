namespace EasyRent.Configuration;

public interface IEnvironmentProvider
{
    string Stage { get; }
    string ApplicationUrls { get; }

    bool IsDev { get; }
    bool IsHost { get; }
    bool IsProd { get; }
}