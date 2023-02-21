using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions;
using Newtonsoft.Json;
using NLog;
using RabbitMQ.Client;
using ILogger = EasyRent.Infrastructure.Abstractions.Abstractions.ILogger;

namespace EasyRent.MessageBroker.RabbitMq;

public class RabbitMqMessagePublisher : IMessagePublisher
{
    private readonly ILogger _logger;
    private readonly IModel _channel;

    public RabbitMqMessagePublisher(ILogger logger,
        IModel channel)
    {
        _logger = logger;
        _channel = channel;
    }

    public Task PublishMessageAsync(string exchangeName,
        IMessage message,
        CancellationToken cancellationToken = default)
    {
        var fullExchangeName = $"{exchangeName}.exchange";

        using (ScopeContext.PushProperty(LoggingScope.MessageBroker.ScopeName,
                   LoggingScope.MessageBroker.ParsePublishScopeMessage(fullExchangeName,
                       message.MessageId,
                       message.Type)))
        {
            _channel.ExchangeDeclare(exchange: fullExchangeName,
                type: ExchangeType.Fanout,
                durable: true);

            var serializedMessage = JsonConvert.SerializeObject(message);
            var encodedSerializedMessage = Encoding.UTF8.GetBytes(serializedMessage);

            _channel.BasicPublish(exchange: fullExchangeName,
                routingKey: string.Empty,
                body: encodedSerializedMessage);

            _logger.Info("Message published to the exchange successfully");
            _logger.Trace("Serialized message data: {SerializedMessage}", serializedMessage);
        }

        return Task.CompletedTask;
    }

    public void Dispose() => _channel?.Close();
}