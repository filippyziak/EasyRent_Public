using EasyRent.Configuration;
using EasyRent.EventSourcing.EventStore;
using EasyRent.Infrastructure;
using EasyRent.MessageBroker;
using EasyRent.MessageBroker.RabbitMq;
using EasyRent.Modules;
using EasyRent.RentalAd.Core.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Configurations;
using EasyRent.RentalAd.Infrastructure.Facades;
using EasyRent.RentalAd.Infrastructure.Integrations;
using EasyRent.RentalAd.Infrastructure.Projections.RentalAd;
using EasyRent.RentalAd.Infrastructure.Repositories.Core;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.RentalAd.Shared.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.RentalAd;

public class RentalAdModule : ModuleManifest
{
    public override string Name => "RentalAd";

    public override void Register(IServiceCollection services, IConfiguration configuration, IEnvironmentProvider environmentProvider)
    {
        services.AddCloudinaryFileStorageSertvice(configuration);
        
        services
            .AddProjection<RentalAdArchivedProjection, RentalAdArchived>()
            .AddProjection<RentalAdCreatedProjection, RentalAdCreated>()
            .AddProjection<RentalAdDescriptionUpdatedProjection, RentalAdDescriptionUpdated>()
            .AddProjection<RentalAdMainPictureSetProjection, RentalAdMainPictureSet>()
            .AddProjection<RentalAdPictureAddedProjection, RentalAdPictureAdded>()
            .AddProjection<RentalAdPlaceAddressCityUpdatedProjection, RentalAdPlaceAddressCityUpdated>()
            .AddProjection<RentalAdPlaceAddressCountryUpdatedProjection, RentalAdPlaceAddressCountryUpdated>()
            .AddProjection<RentalAdPlaceAddressCreatedProjection, RentalAdPlaceAddressCreated>()
            .AddProjection<RentalAdPlaceAddressStreetUpdatedProjection, RentalAdPlaceAddressStreetUpdated>()
            .AddProjection<RentalAdPlaceFeatureAddedProjection, RentalAdPlaceFeatureAdded>()
            .AddProjection<RentalAdPlaceFeatureDescriptionUpdatedProjection, RentalAdPlaceFeatureDescriptionUpdated>()
            .AddProjection<RentalAdPlaceFeaturePictureUrlUpdatedProjection, RentalAdPlaceFeaturePictureUrlUpdated>()
            .AddProjection<RentalAdPlaceFeatureRemovedProjection, RentalAdPlaceFeatureRemoved>()
            .AddProjection<RentalAdPlaceOwnerCreatedProjection, RentalAdPlaceOwnerCreated>()
            .AddProjection<RentalAdPlaceOwnerDeletedProjection, RentalAdPlaceOwnerDeleted>()
            .AddProjection<RentalAdPlaceOwnerEmailAddressUpdatedProjection, RentalAdPlaceOwnerEmailAddressUpdated>()
            .AddProjection<RentalAdPlaceOwnerPictureUrlUpdatedProjection, RentalAdPlaceOwnerPictureUrlUpdated>()
            .AddProjection<RentalAdPlaceReservationArchivedProjection, RentalAdPlaceReservationArchived>()
            .AddProjection<RentalAdPlaceReservationCreatedProjection, RentalAdPlaceReservationCreated>()
            .AddProjection<RentalAdPlaceReservationRemovedProjection, RentalAdPlaceReservationRemoved>()
            .AddProjection<RentalAdPlaceReservationReviewScoreUpdatedProjection, RentalAdPlaceReservationReviewScoreUpdated>()
            .AddProjection<RentalAdPlaceReservationReviewUpdatedProjection, RentalAdPlaceReservationReviewUpdated>()
            .AddProjection<RentalAdPricePerDayUpdatedProjection, RentalAdPricePerDayUpdated>()
            .AddProjection<RentalAdPlacePictureRemovedProjection, RentalAdPlacePictureRemoved>()
            .AddProjection<RentalAdMainPictureRemovedProjection, RentalAdMainPictureRemoved>()
            .AddProjection<RentalAdTitleUpdatedProjection, RentalAdTitleUpdated>();

        services
            .AddScoped<IPlaceFeatureReadOnlyRepository, PlaceFeatureReadOnlyRepository>()
            .AddScoped<IRentalAdReadOnlyRepository, RentalAdReadOnlyRepository>()
            .AddScoped<IPlaceOwnerReadOnlyRepository, PlaceOwnerReadOnlyRepository>();

        services.AddScoped<IRentalAdContextFacade, RentalAdContextFacade>();

        services.AddRabbitMqTopicMessagePublisherAndConsumer(configuration)
            .AddMessageConsumer<IdentityConsumer>()
            .AddMessageConsumer<ReservationConsumer>();

        services.AddMongoDbDocumentRepository<IRentalAdDocumentRepository,
            RentalAdDocumentRepository,
            RentalAdDocumentStoreConfiguration>(configuration);
    }
}