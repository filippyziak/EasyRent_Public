namespace EasyRent.Infrastructure.Abstractions.Database;

public abstract record DatabaseConfiguration
{
    public string ConnectionString { get; init; }
}