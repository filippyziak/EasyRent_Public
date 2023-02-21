using EasyRent.Infrastructure.Abstractions.Database;

namespace EasyRent.Infrastructure.Abstractions.DocumentStore;

public record DocumentStoreConfiguration : DatabaseConfiguration
{
    public string DatabaseName { get; init; }
}