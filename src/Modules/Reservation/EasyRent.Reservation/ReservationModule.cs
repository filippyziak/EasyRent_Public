using EasyRent.Configuration;
using EasyRent.EventSourcing.EventStore;
using EasyRent.Infrastructure;
using EasyRent.MessageBroker.RabbitMq;
using EasyRent.Modules;
using EasyRent.Reservation.Core.Abstractions;
using EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;
using EasyRent.Reservation.Infrastructure.BackgroundServices;
using EasyRent.Reservation.Infrastructure.Configurations;
using EasyRent.Reservation.Infrastructure.Projections.PlaceReservation.V1;
using EasyRent.Reservation.Infrastructure.Repositories.Core;
using EasyRent.Reservation.Infrastructure.Repositories.DocumentStore;
using EasyRent.Reservation.Infrastructure.Repositories.DocumentStore.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.Reservation;

public class ReservationModule : ModuleManifest
{
    public override string Name => "Reservation";

    public override void Register(IServiceCollection services, IConfiguration configuration, IEnvironmentProvider environmentProvider)
    {
        services
            .AddProjection<PlaceReservationArchivedProjection, PlaceReservationArchived>()
            .AddProjection<PlaceReservationCreatedProjection, PlaceReservationCreated>()
            .AddProjection<PlaceReservationFinishedProjection, PlaceReservationFinished>()
            .AddProjection<PlaceReservationPaidProjection, PlaceReservationPaid>()
            .AddProjection<PlaceReservationReviewDescriptionUpdatedProjection, PlaceReservationReviewDescriptionUpdated>()
            .AddProjection<PlaceReservationReviewedProjection, PlaceReservationReviewed>()
            .AddProjection<PlaceReservationReviewScoreUpdatedProjection, PlaceReservationReviewScoreUpdated>();

        services.AddScoped<IRentalAdReadOnlyRepository, RentalAdReadOnlyRepository>();
        services.AddScoped<IReservationReadOnlyRepository, ReservationReadOnlyRepository>();
        services.AddScoped<ITenantReadOnlyRepository, TenantReadOnlyRepository>();

        services.AddHostedService<ReservationsBackgroundService>();

        services.AddRabbitMqTopicMessagePublisherAndConsumer(configuration);

        services.AddMongoDbDocumentRepository<IPlaceReservationDocumentRepository,
            PlaceReservationDocumentRepository,
            PlaceReservationDocumentConfiguration>(configuration);
    }
}