using System.Collections.Generic;

namespace EasyRent.NetCore.Cors;

public record CorsConfiguration
{
    private const string DefaultPolicy = "Default";

    public Dictionary<string, string[]> OriginsPolicies { get; init; } = new()
    {
        [DefaultPolicy] = new[] { "http://localhost:4200/", "http://host.docker.internal:4200" }
    };
}