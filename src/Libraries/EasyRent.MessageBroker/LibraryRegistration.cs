using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.MessageBroker;

public static class LibraryRegistration
{
    public static IServiceCollection AddMessageConsumer<TTopicMessageListener>(this IServiceCollection services)
        where TTopicMessageListener : class, IMessageConsumer
        => services.AddSingleton<IMessageConsumer, TTopicMessageListener>();

    public static IServiceCollection AddMessageBrokerConsumerHostedService(this IServiceCollection services)
        => services.AddHostedService<MessageBrokerConsumerHostedService>();

    public static IServiceCollection ConfigureMessageBroker(this IServiceCollection services,
        IConfiguration configuration)
        => services.Configure<MessageBrokerConfiguration>(configuration.GetSection(nameof(MessageBrokerConfiguration)));
}