using System.Threading;
using System.Threading.Tasks;

namespace EasyRent.MessageBroker;

public interface IMessageConsumer
{
    string ExchangeName { get; }

    Task StartConsumingAsync(CancellationToken cancellationToken);
    Task StopConsumingAsync(CancellationToken cancellationToken);
}