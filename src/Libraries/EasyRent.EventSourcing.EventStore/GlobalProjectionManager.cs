using System.Threading.Tasks;
using EasyRent.EventSourcing.EventStore.Checkpoints;
using EventStore.ClientAPI;
using ILogger = EasyRent.Infrastructure.Abstractions.Abstractions.ILogger;

namespace EasyRent.EventSourcing.EventStore;

public class GlobalProjectionManager : IProjectionManager
{
    private const string SubscriptionName = "try-out-subscription";
    private const int MaxLiveQueueSize = 2000;
    private const int ReadBatchSize = 500;

    private readonly ILogger _logger;
    private readonly IEventStoreConnection _storeConnection;
    private readonly IProjectionStrategy _projectionStrategy;
    private readonly ICheckpointStore _checkpointStore;

    private EventStoreAllCatchUpSubscription _subscription;

    public GlobalProjectionManager(ILogger logger,
        IEventStoreConnection storeConnection,
        IProjectionStrategy projectionStrategy,
        ICheckpointStore checkpointStore)
    {
        _logger = logger;
        _storeConnection = storeConnection;
        _projectionStrategy = projectionStrategy;
        _checkpointStore = checkpointStore;
    }

    public async Task StartAsync()
    {
        var settings = new CatchUpSubscriptionSettings(MaxLiveQueueSize,
            ReadBatchSize,
            true,
            false,
            SubscriptionName);

        var position = await _checkpointStore.GetCheckpointAsync();

        _logger.Info("Subscribing all events from position: {Position}. Subscription name: {SubscriptionName}",
            position.CommitPosition,
            SubscriptionName);

        _subscription = _storeConnection.SubscribeToAllFrom(position,
            settings,
            EventAppearedAsync);
    }

    public void Stop() => _subscription.Stop();

    private async Task EventAppearedAsync(EventStoreCatchUpSubscription _, ResolvedEvent resolvedEvent)
    {
        if (resolvedEvent.Event.EventType.StartsWith("$"))
            return;

        var @event = resolvedEvent.Deserialize();

        await _projectionStrategy.ProjectEventAsync(@event);

        await _checkpointStore.StoreCheckpointAsync(resolvedEvent.OriginalPosition.Value);
        _logger.Trace("> Global checkpoint position updated. Current position: {Position}",
            resolvedEvent.OriginalPosition.Value);
    }
}