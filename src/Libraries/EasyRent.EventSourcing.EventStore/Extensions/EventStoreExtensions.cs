using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace EasyRent.EventSourcing.EventStore.Extensions;

public static class EventStoreExtensions
{
    public static Task AppendEventsAsync(this IEventStoreConnection connection,
        string streamName,
        long version,
        params IDomainEvent[] events)
    {
        if (events is null || !events.Any())
            return Task.CompletedTask;

        var preparedEvents = events
            .Select(domainEvent =>
                new EventData(
                    eventId: Guid.NewGuid(),
                    type: domainEvent.GetType().Name,
                    isJson: true,
                    data: EventSerializer.Serialize(domainEvent),
                    metadata: EventSerializer.Serialize(new EventMetadata
                    {
                        ClrType = domainEvent.GetType().AssemblyQualifiedName
                    })))
            .ToImmutableArray();

        return connection.AppendToStreamAsync(
            streamName,
            version,
            preparedEvents);
    }
}