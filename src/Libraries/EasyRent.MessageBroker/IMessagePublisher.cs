using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyRent.MessageBroker;

public interface IMessagePublisher : IDisposable
{
    Task PublishMessageAsync(string exchangeName,
        IMessage message,
        CancellationToken cancellationToken = default);
}