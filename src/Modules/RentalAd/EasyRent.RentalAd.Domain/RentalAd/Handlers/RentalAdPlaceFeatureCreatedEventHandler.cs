using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public sealed class RentalAdPlaceFeatureCreatedEventHandler : DomainEventHandler<RentalAd, RentalAdPlaceFeatureAdded>
{
    public RentalAdPlaceFeatureCreatedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceFeatureAdded domainEvent)
    {
        var placeFeature = new PlaceFeature(Entity.Apply);
        Entity.ApplyToEntity(placeFeature, domainEvent);
        Entity.PlaceFeatures.Add(placeFeature);
    }
}

public sealed class PlaceFeatureRentalAdCreatedEventHandler : DomainEventHandler<PlaceFeature, RentalAdPlaceFeatureAdded>
{
    public PlaceFeatureRentalAdCreatedEventHandler(PlaceFeature entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceFeatureAdded domainEvent)
    {
        Entity.RentalAdId = new RentalAdId(domainEvent.RentalAdId);
        Entity.PlaceFeatureId = new PlaceFeatureId(domainEvent.PlaceFeatureId);
        Entity.Description = new PlaceFeatureDescription(domainEvent.Description);
        Entity.PictureUrl = new PlaceFeaturePictureUrl(domainEvent.PictureUrl);
    }
}