using EasyRent.Configuration;
using EasyRent.EventSourcing.EventStore;
using EasyRent.Infrastructure;
using EasyRent.Management.Core.Abstractions;
using EasyRent.Management.Domain.Accounts.DomainEvents.V1;
using EasyRent.Management.Domain.PlaceFeatures.DomainEvents.V1;
using EasyRent.Management.Infrastructure.Configurations;
using EasyRent.Management.Infrastructure.Facades;
using EasyRent.Management.Infrastructure.Integrations;
using EasyRent.Management.Infrastructure.Projections.Accounts.V1;
using EasyRent.Management.Infrastructure.Projections.PlaceFeatures.V1;
using EasyRent.Management.Infrastructure.Repositories.Core;
using EasyRent.Management.Infrastructure.Repositories.DocumentStore;
using EasyRent.Management.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.Management.Shared.Abstractions;
using EasyRent.MessageBroker;
using EasyRent.MessageBroker.RabbitMq;
using EasyRent.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.Management;

public class ManagementModule : ModuleManifest
{
    public override string Name => "Management";

    public override void Register(IServiceCollection services, IConfiguration configuration, IEnvironmentProvider environmentProvider)
    {
        services
            .AddProjection<AccountActivatedProjection, AccountActivated>()
            .AddProjection<AccountArchivedProjection, AccountArchived>()
            .AddProjection<AccountCreatedProjection, AccountCreated>()
            .AddProjection<AccountSuspendedProjection, AccountSuspended>()
            .AddProjection<AccountTypeChangedProjection, AccountTypeChanged>()
            .AddProjection<PlaceFeatureCreatedProjection, PlaceFeatureCreated>();

        services.AddScoped<IPlaceFeatureReadOnlyRepository, PlaceFeatureReadOnlyRepository>();
        services.AddScoped<IManagementContextFacade, ManagementContextFacade>();

        services.AddRabbitMqTopicMessagePublisherAndConsumer(configuration)
            .AddMessageConsumer<IdentityConsumer>();

        services.AddMongoDbDocumentRepository<IManagementAccountDocumentRepository,
            ManagementAccountDocumentRepository,
            ManagementDocumentConfiguration>(configuration);
        
        services.AddMongoDbDocumentRepository<IPlaceFeatureDocumentRepository,
            PlaceFeatureDocumentRepository,
            ManagementDocumentConfiguration>(configuration);
    }
}