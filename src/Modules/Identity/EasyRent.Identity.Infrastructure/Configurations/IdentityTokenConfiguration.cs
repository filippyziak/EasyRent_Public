namespace EasyRent.Identity.Infrastructure.Configurations;

public record IdentityTokenConfiguration
{
    public string SecretKey { get; init; }
    public int TokenExpirationTimeInMinutes { get; init; } = 60;
}