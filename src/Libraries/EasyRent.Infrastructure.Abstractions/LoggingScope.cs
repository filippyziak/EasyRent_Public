using System;

namespace EasyRent.Infrastructure.Abstractions;

public class LoggingScope
{
    public static class Request
    {
        public const string ScopeName = "request";

        public static string ParseScopeMessage(params string[] parameters)
            => $"{string.Join(" | ", parameters)} | ";
    }

    public static class Projection
    {
        public const string ScopeName = "projection";

        public static string ParseScopeMessage(object @event, Type projectionType)
            => $"{projectionType.FullName} | {@event.GetType().Name} | ";
    }

    public static class HostedService
    {
        public const string ScopeName = "hostedservice";

        public static string ParseScopeMessage(Type hostedServiceType)
            => $"{hostedServiceType.Name} | ";
    }

    public static class MessageBroker
    {
        public const string ScopeName = "messagebroker";

        public static string ParsePublishScopeMessage(string exchangeName, string messageId, string type)
            => $"{exchangeName} | {type} | {messageId} | >> ";

        public static string ParseSubscribeScopeMessage(Type consumerType, string exchangeName, string messageId, string type)
            => $"{consumerType.FullName} | {exchangeName} | {type} | {messageId} | << ";

        public static class MessageConsumer
        {
            public const string TopicMessageListenerScopeName = "messageconsumer";

            public static string ParseScopeMessage(Type consumerType)
                => $"{consumerType.FullName} | ";
        }
    }
}