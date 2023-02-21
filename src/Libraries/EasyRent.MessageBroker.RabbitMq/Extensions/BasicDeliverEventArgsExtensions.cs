using System.Text;
using RabbitMQ.Client.Events;

namespace EasyRent.MessageBroker.RabbitMq.Extensions;

public static class BasicDeliverEventArgsExtensions
{
    public static string GetStringFromMessageBody(this BasicDeliverEventArgs basicDeliverEventArgs)
        => Encoding.UTF8.GetString(basicDeliverEventArgs.Body.ToArray());
}