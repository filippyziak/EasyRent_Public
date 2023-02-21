using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public class RentalAdPlaceFeatureDescriptionUpdatedEventHandler : DomainEventHandler<PlaceFeature, RentalAdPlaceFeatureDescriptionUpdated>
{
    public RentalAdPlaceFeatureDescriptionUpdatedEventHandler(PlaceFeature entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceFeatureDescriptionUpdated domainEvent)
    {
        Entity.Description = new PlaceFeatureDescription(domainEvent.NewDescription);
    }
}