using System;

namespace EasyRent.Configuration;

public class EnvironmentProvider : IEnvironmentProvider
{
    public string Stage => (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Stages.Dev)
        .ToLowerInvariant();

    public string ApplicationUrls => Environment.GetEnvironmentVariable("ASPNETCORE_URLS");

    public bool IsDev => Stage.Equals(Stages.Dev, StringComparison.InvariantCultureIgnoreCase);
    public bool IsHost => Stage.Equals(Stages.Host, StringComparison.InvariantCultureIgnoreCase);
    public bool IsProd => Stage.Equals(Stages.Prod, StringComparison.InvariantCultureIgnoreCase);

    private static class Stages
    {
        public const string Dev = "dev";
        public const string Host = "host";
        public const string Prod = "prod";
    }
}