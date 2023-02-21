using RabbitMQ.Client;

namespace EasyRent.MessageBroker.RabbitMq.Factories;

public static class ConnectionFactory
{
    public static IConnection OpenConnection(this RabbitMQ.Client.ConnectionFactory connectionFactory)
        => connectionFactory.CreateConnection();

    public static IModel OpenChannel(this IConnection connection)
        => connection.CreateModel();
}