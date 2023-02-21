using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public class RentalAdPlaceOwnerPictureUrlUpdatedEventHandler : DomainEventHandler<PlaceOwner, RentalAdPlaceOwnerPictureUrlUpdated>
{
    public RentalAdPlaceOwnerPictureUrlUpdatedEventHandler(PlaceOwner entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceOwnerPictureUrlUpdated domainEvent)
    {
        Entity.PictureUrl = PlaceOwnerPictureUrl.FromString(domainEvent.NewPictureUrl);
    }
}