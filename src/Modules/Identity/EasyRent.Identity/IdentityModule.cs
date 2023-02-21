using EasyRent.Configuration;
using EasyRent.EventSourcing.EventStore;
using EasyRent.Identity.Core.Abstractions;
using EasyRent.Identity.Domain.Account.DomainEvents.V1;
using EasyRent.Identity.Domain.DomainServices;
using EasyRent.Identity.Infrastructure.BackgroundServices;
using EasyRent.Identity.Infrastructure.Configurations;
using EasyRent.Identity.Infrastructure.DomainService;
using EasyRent.Identity.Infrastructure.Facades;
using EasyRent.Identity.Infrastructure.Integrations;
using EasyRent.Identity.Infrastructure.Projections.Account.V1;
using EasyRent.Identity.Infrastructure.Registration;
using EasyRent.Identity.Infrastructure.Repositories.Core;
using EasyRent.Identity.Infrastructure.Repositories.DocumentStore;
using EasyRent.Identity.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.Identity.Infrastructure.Services;
using EasyRent.Identity.Shared.Abstractions;
using EasyRent.Infrastructure;
using EasyRent.MessageBroker;
using EasyRent.MessageBroker.RabbitMq;
using EasyRent.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.Identity;

public class IdentityModule : ModuleManifest
{
    public override string Name => "Identity";

    public override void Register(IServiceCollection services, IConfiguration configuration, IEnvironmentProvider environmentProvider)
    {
        services.Configure<IdentityTokenConfiguration>(configuration.GetSection(nameof(IdentityTokenConfiguration)));

        services
            .AddSingleton<IHashProvider, Argon2HashProvider>()
            .AddSingleton<IAuthTokenProvider, JwtAuthTokenProvider>();

        services.AddJwtBearerAuthentication(configuration);
        services.AddJwtBearerAuthorization();

        services
            .AddProjection<AccountActivatedProjection, AccountActivated>()
            .AddProjection<AccountArchivedProjection, AccountArchived>()
            .AddProjection<AccountCreatedProjection, AccountCreated>()
            .AddProjection<AccountEmailAddressUpdatedProjection, AccountEmailAddressUpdated>()
            .AddProjection<AccountPasswordDataUpdatedProjection, AccountPasswordDataUpdated>()
            .AddProjection<AccountSuspendedProjection, AccountSuspended>()
            .AddProjection<AccountTypeUpdatedProjection, AccountTypeUpdated>();

        services.AddScoped<IAccountReadOnlyRepository, AccountReadOnlyRepository>();
        services.AddScoped<IIdentityContextFacade, IdentityContextFacade>();

        services.AddRabbitMqTopicMessagePublisherAndConsumer(configuration)
            .AddMessageConsumer<ManagementConsumer>();

        services.AddHostedService<IdentityDataSeederBackgroundService>();

        services.AddMongoDbDocumentRepository<IAccountDocumentRepository,
            AccountDocumentRepository,
            AccountDocumentConfiguration>(configuration);
    }

    public override void Use(WebApplication app, IEnvironmentProvider environmentProvider)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}