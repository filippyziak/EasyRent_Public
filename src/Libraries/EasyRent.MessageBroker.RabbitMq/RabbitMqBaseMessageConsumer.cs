using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions;
using EasyRent.MessageBroker.RabbitMq.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ILogger = EasyRent.Infrastructure.Abstractions.Abstractions.ILogger;

namespace EasyRent.MessageBroker.RabbitMq;

public abstract class RabbitMqBaseMessageConsumer<TMessage> : IMessageConsumer
    where TMessage : IMessage
{
    private const string DlqExchangeNameSuffix = "dlq";

    protected readonly ILogger Logger;
    private readonly IOptionsMonitor<MessageBrokerConfiguration> _messageBrokerConfigurationOptions;
    private readonly IModel _channel;

    private AsyncEventingBasicConsumer _consumer;

    public RabbitMqBaseMessageConsumer(ILogger logger,
        IOptionsMonitor<MessageBrokerConfiguration> messageBrokerConfigurationOptions,
        IModel channel)
    {
        Logger = logger;
        _messageBrokerConfigurationOptions = messageBrokerConfigurationOptions;
        _channel = channel;
    }

    public abstract string ExchangeName { get; }

    private string FullExchangeName => $"{ExchangeName}.exchange".ToLowerInvariant();
    private string QueueName => $"{ExchangeName}.queue.{this.GetType().FullName}".ToLowerInvariant();
    private string DlqExchangeName => $"{ExchangeName}.exchange.{DlqExchangeNameSuffix}".ToLowerInvariant();
    private string DlqQueueName => $"{ExchangeName}.queue.{DlqExchangeNameSuffix}".ToLowerInvariant();

    public Task StartConsumingAsync(CancellationToken cancellationToken)
    {
        try
        {
            _channel.ExchangeDeclare(FullExchangeName,
                ExchangeType.Fanout,
                durable: true);
            _channel.ExchangeDeclare(DlqExchangeName,
                ExchangeType.Fanout,
                durable: true);

            ConsumeQueue(cancellationToken);
        }
        catch (Exception e)
        {
            Logger.Error("An error occurred during consuming messages incoming from the exchange: {ExchangeName}", e,
                FullExchangeName);
        }

        return Task.CompletedTask;
    }

    public async Task StopConsumingAsync(CancellationToken cancellationToken)
    {
        ScopeContext.Clear();
        using (ScopeContext.PushProperty(LoggingScope.MessageBroker.MessageConsumer.TopicMessageListenerScopeName,
                   LoggingScope.MessageBroker.MessageConsumer.ParseScopeMessage(this.GetType())))
        {
            _channel.Close();

            Logger.Info("Stopping consuming messages incoming from the exchange: {ExchangeName}", FullExchangeName);

            await Task.CompletedTask;
        }
    }

    protected abstract Task<bool> OnMessageReceivedAsync(TMessage message, CancellationToken cancellationToken);

    private void ConsumeQueue(CancellationToken cancellationToken)
    {
        var queueName = _channel.QueueDeclare(QueueName,
            durable: true,
            autoDelete: false,
            exclusive: false,
            arguments: new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", DlqExchangeName },
                { "x-message-ttl", _messageBrokerConfigurationOptions.CurrentValue.DlqMessageTtlInMilliseconds }
            }).QueueName;
        var dlqQueueName = _channel.QueueDeclare(DlqQueueName,
            true,
            false,
            false).QueueName;

        _channel.QueueBind(queue: queueName,
            exchange: FullExchangeName,
            routingKey: string.Empty);
        _channel.QueueBind(queue: dlqQueueName,
            DlqExchangeName,
            dlqQueueName);

        Logger.Info("Exchange: {ExchangeName} is waiting for the messages", FullExchangeName);

        _consumer = new AsyncEventingBasicConsumer(_channel);
        _consumer.Received += async (_, basicDeliverEventArgs)
            => await HandleReceivedMessageAsync(basicDeliverEventArgs, cancellationToken);

        _channel.BasicConsume(queue: queueName,
            autoAck: false,
            consumer: _consumer);
    }

    private async Task HandleReceivedMessageAsync(BasicDeliverEventArgs basicDeliverEventArgs, CancellationToken cancellationToken)
    {
        try
        {
            var deserializedBodyString = basicDeliverEventArgs.GetStringFromMessageBody();
            var message = JsonConvert.DeserializeObject<TMessage>(deserializedBodyString);

            ScopeContext.Clear();
            using (ScopeContext.PushProperty(LoggingScope.MessageBroker.ScopeName,
                       LoggingScope.MessageBroker.ParseSubscribeScopeMessage(this.GetType(),
                           basicDeliverEventArgs.Exchange,
                           message.MessageId,
                           message.Type)))
            {
                Logger.Info("Message received from the queue successfully");
                var messageProcessed = await OnMessageReceivedAsync(message, cancellationToken);

                if (messageProcessed)
                {
                    _channel.BasicAck(basicDeliverEventArgs.DeliveryTag,
                        multiple: false);
                    Logger.Trace("Message acknowledged as processed properly and removed from the queue");
                }
                else
                {
                    Logger.Error("Message has not been processed correctly");
                    Logger.Warning("Sending error message to the DLQ queue: {DlqQueueName}. Message:\n{Message}",
                        DlqQueueName,
                        basicDeliverEventArgs.GetStringFromMessageBody());
                    _channel.BasicNack(basicDeliverEventArgs.DeliveryTag,
                        multiple: false,
                        requeue: false);
                }
            }
        }
        catch (Exception e)
        {
            Logger.Error("An error occurred during processing the message", e);
            Logger.Warning("Sending error message to the DLQ queue: {DlqQueueName}. Message:\n{Message}",
                DlqQueueName,
                basicDeliverEventArgs.GetStringFromMessageBody());
            _channel.BasicNack(basicDeliverEventArgs.DeliveryTag, multiple: false, requeue: false);
        }
    }
}