namespace EasyRent.EventSourcing.EventStore;

public record EventMetadata
{
    public string ClrType { get; init; }
}