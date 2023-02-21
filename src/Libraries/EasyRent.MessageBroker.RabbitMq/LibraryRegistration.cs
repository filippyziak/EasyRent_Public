using EasyRent.MessageBroker.RabbitMq.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EasyRent.MessageBroker.RabbitMq;

public static class LibraryRegistration
{
    public static IServiceCollection AddRabbitMqTopicMessagePublisherAndConsumer(this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddSingleton<IMessagePublisher, RabbitMqMessagePublisher>()
            .AddMessageBrokerConsumerHostedService()
            .AddSingleton<IModel>(serviceProvider => ConnectionFactoryCreator.InitializeAsyncConnectionFactory(
                    serviceProvider.GetRequiredService<IOptionsMonitor<MessageBrokerConfiguration>>().CurrentValue)
                .OpenConnection()
                .CreateModel())
            .ConfigureMessageBroker(configuration);
}