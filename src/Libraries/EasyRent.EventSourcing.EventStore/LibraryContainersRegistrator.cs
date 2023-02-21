using System.Linq;
using EasyRent.EventSourcing.EventStore.Checkpoints;
using EasyRent.EventSourcing.EventStore.Configurations;
using EasyRent.Infrastructure;
using EasyRent.Infrastructure.Abstractions.DocumentStore;
using EasyRent.Infrastructure.Abstractions.Extensions;
using EasyRent.Infrastructure.DocumentStore;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.EventSourcing.EventStore;

public static class LibraryContainersRegistrator
{
    public static IServiceCollection AddEventStore(this IServiceCollection services, IConfiguration configuration)
    {
        var eventStoreConnection = EventStoreConnection.Create(
            configuration.GetConnectionString<EventStoreConfiguration>(),
            ConnectionSettings
                .Create()
                .KeepReconnecting());

        var aggregateRepository = new EventStoreAggregateRepository(eventStoreConnection);

        var eventStoreConfiguration = new EventStoreConfiguration();
        configuration.GetSection(nameof(EventStoreConfiguration)).Bind(eventStoreConfiguration);

        services.AddMongoDbDocumentRepository<IDocumentRepository<Checkpoint>,
            MongoDbDocumentRepository<Checkpoint>,
            CheckpointStoreConfiguration>(configuration);

        services
            .AddSingleton<IEventStoreConnection>(eventStoreConnection)
            .AddSingleton<IAggregateRepository>(aggregateRepository)
            .AddSingleton<ICheckpointStore>(provider => new MongoDbCheckpointStore(eventStoreConfiguration.GlobalCheckpointId,
                provider.GetServices<IDocumentRepository<Checkpoint>>()
                    .Single(repository => repository is MongoDbDocumentRepository<Checkpoint>)));

        services.AddSingleton<IProjectionStrategy, DefaultProjectionStrategy>();

        services.AddSingleton<IProjectionManager, GlobalProjectionManager>();

        services.AddHostedService<EventStoreBackgroundService>();

        return services;
    }

    public static IServiceCollection AddProjection<TProjection, TEventType>(this IServiceCollection services)
        where TProjection : DefaultProjection<TEventType>, IProjection
        => services.AddSingleton<IProjection, TProjection>();
}