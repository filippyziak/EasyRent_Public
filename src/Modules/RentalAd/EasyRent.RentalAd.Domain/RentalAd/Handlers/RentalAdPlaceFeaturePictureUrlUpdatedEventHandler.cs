using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public class RentalAdPlaceFeaturePictureUrlUpdatedEventHandler : DomainEventHandler<PlaceFeature, RentalAdPlaceFeaturePictureUrlUpdated>
{
    public RentalAdPlaceFeaturePictureUrlUpdatedEventHandler(PlaceFeature entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceFeaturePictureUrlUpdated domainEvent)
    {
        Entity.PictureUrl = new PlaceFeaturePictureUrl(domainEvent.NewPictureUrl);
    }
}