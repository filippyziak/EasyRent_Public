namespace EasyRent.MessageBroker.RabbitMq.Factories;

public static class ConnectionFactoryCreator
{
    public static RabbitMQ.Client.ConnectionFactory InitializeAsyncConnectionFactory(MessageBrokerConfiguration messageBrokerConfiguration)
        => new RabbitMQ.Client.ConnectionFactory
        {
            HostName = messageBrokerConfiguration.HostName,
            Port = messageBrokerConfiguration.Port,
            DispatchConsumersAsync = true,
            AutomaticRecoveryEnabled = true
        };
}