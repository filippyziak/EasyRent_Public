using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRent.EventSourcing.EventStore.Extensions;
using EventStore.ClientAPI;

namespace EasyRent.EventSourcing.EventStore;

public class EventStoreAggregateRepository : IAggregateRepository
{
    private IEventStoreConnection _connection;

    public EventStoreAggregateRepository(IEventStoreConnection connection) => _connection = connection;

    public async Task<T> LoadAsync<T, TId>(TId aggregateId) where T : AggregateRoot<TId> where TId : AggregateId
    {
        ArgumentNullException.ThrowIfNull(aggregateId);

        var streamName = GetStreamName<T, TId>(aggregateId);
        var aggregate = (T)Activator.CreateInstance(typeof(T), nonPublic: true);

        var page = await _connection.ReadStreamEventsForwardAsync(
            streamName, 0, 1024, false);

        aggregate.Load(page.Events.Select(
            resolvedEvent => resolvedEvent.Deserialize()));

        return aggregate;
    }

    public async Task SaveAsync<T, TId>(T aggregate) where T : AggregateRoot<TId> where TId : AggregateId
    {
        ArgumentNullException.ThrowIfNull(aggregate);

        var streamName = GetStreamName<T, TId>(aggregate);
        var changes = aggregate.GetDomainEventsHistory();

        await _connection.AppendEventsAsync(streamName, aggregate.Version, changes.ToArray());

        aggregate.ClearDomainEventsHistory();
    }

    public async Task<bool> ExistsAsync<T, TId>(TId aggregateId) where T : AggregateRoot<TId> where TId : AggregateId
    {
        var streamName = GetStreamName<T, TId>(aggregateId);
        var result = await _connection.ReadEventAsync(streamName, 0, false);

        return result.Status != EventReadStatus.NoStream;
    }

    private string GetStreamName<T, TId>(TId aggregateId)
        where TId : AggregateId
        => $"{typeof(T).Name}-{aggregateId.Value}";

    private string GetStreamName<T, TId>(T aggregate)
        where T : AggregateRoot<TId>
        where TId : AggregateId
    {
        var aggregateIdProperty = aggregate
                                      .GetType()
                                      .GetProperties()
                                      .FirstOrDefault(p => p.PropertyType == typeof(TId))
                                  ?? throw new KeyNotFoundException("Aggregate key is not found");

        var aggregateIdValue = ((AggregateId)aggregateIdProperty.GetValue(aggregate))?.Value
                               ?? throw new KeyNotFoundException("Aggregate key is not found");

        return $"{typeof(T).Name}-{aggregateIdValue}";
    }
}