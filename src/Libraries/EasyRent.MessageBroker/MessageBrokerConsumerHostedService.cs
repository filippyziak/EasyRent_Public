using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions;
using Microsoft.Extensions.Hosting;
using NLog;
using ILogger = EasyRent.Infrastructure.Abstractions.Abstractions.ILogger;

namespace EasyRent.MessageBroker;

public class MessageBrokerConsumerHostedService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IReadOnlyList<IMessageConsumer> _messageConsumers;

    public MessageBrokerConsumerHostedService(ILogger logger,
        IEnumerable<IMessageConsumer> messageConsumers)
    {
        _logger = logger;
        _messageConsumers = messageConsumers.ToList();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (ScopeContext.PushProperty(LoggingScope.HostedService.ScopeName,
                   LoggingScope.HostedService.ParseScopeMessage(typeof(MessageBrokerConsumerHostedService))))
        {
            try
            {
                _logger.Info("Starting consuming incoming messages from the following exchanges: [{Exchanges}]",
                    string.Join(',', _messageConsumers.Select(messageConsumer => messageConsumer.ExchangeName)));

                var startConsumingTasks = _messageConsumers
                    .Select(messageConsumer => messageConsumer.StartConsumingAsync(cancellationToken))
                    .ToList();

                await Task.WhenAll(startConsumingTasks);

                _logger.Info("{RunningConsumingTasks} message consumers are running and receiving incoming messages",
                    startConsumingTasks.Count);
            }
            catch (Exception e)
            {
                _logger.Error("An error occurred during running message consumers", e);
            }
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using (ScopeContext.PushProperty(LoggingScope.HostedService.ScopeName,
                   LoggingScope.HostedService.ParseScopeMessage(typeof(MessageBrokerConsumerHostedService))))
        {
            _logger.Info("Stopping message consumers...");

            foreach (var messageConsumer in _messageConsumers)
                await messageConsumer.StopConsumingAsync(cancellationToken);

            _logger.Info("Message consumers stopped properly");

            await Task.CompletedTask;
        }
    }
}