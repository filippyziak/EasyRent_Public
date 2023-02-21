using System;
using System.Threading;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Microsoft.Extensions.Hosting;
using ILogger = EasyRent.Infrastructure.Abstractions.Abstractions.ILogger;

namespace EasyRent.EventSourcing.EventStore;

public class EventStoreBackgroundService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IProjectionManager _projectionManager;
    private readonly IEventStoreConnection _storeConnection;

    public EventStoreBackgroundService(ILogger logger,
        IProjectionManager projectionManager,
        IEventStoreConnection storeConnection)
    {
        _logger = logger;
        _projectionManager = projectionManager;
        _storeConnection = storeConnection;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _logger.Info("Connecting to the Event Store...");

            await _storeConnection.ConnectAsync();
            await _projectionManager.StartAsync();

            _logger.Info("Event store connected. Projecting events started");
        }
        catch (Exception e)
        {
            _logger.Error("An error occurred during connecting or projecting events", e);
            throw;
        }
    }
}