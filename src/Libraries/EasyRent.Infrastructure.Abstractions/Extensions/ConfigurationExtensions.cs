using Microsoft.Extensions.Configuration;

namespace EasyRent.Infrastructure.Abstractions.Extensions;

public static class ConfigurationExtensions
{
    public static string GetConnectionString<TSection>(this IConfiguration configuration)
        => configuration.GetSection(typeof(TSection).Name).GetValue<string>("ConnectionString");
}