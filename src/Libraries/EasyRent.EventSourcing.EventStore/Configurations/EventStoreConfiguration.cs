namespace EasyRent.EventSourcing.EventStore.Configurations;

public record EventStoreConfiguration
{
    public string ConnectionString { get; init; }
    public string GlobalCheckpointId { get; init; }
}