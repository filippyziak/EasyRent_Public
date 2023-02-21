using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public sealed class RentalAdPlaceOwnerCreatedEventHandler : DomainEventHandler<RentalAd, RentalAdPlaceOwnerCreated>
{
    public RentalAdPlaceOwnerCreatedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceOwnerCreated domainEvent)
    {
        var placeOwner = new PlaceOwner(Entity.Apply);
        Entity.ApplyToEntity(placeOwner, domainEvent);
        Entity.PlaceOwner = placeOwner;
    }
}

public sealed class PlaceOwnerRentalAdCreatedEventHandler : DomainEventHandler<PlaceOwner, RentalAdPlaceOwnerCreated>
{
    public PlaceOwnerRentalAdCreatedEventHandler(PlaceOwner entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceOwnerCreated domainEvent)
    {
        Entity.RentalAdId = new RentalAdId(domainEvent.RentalAdId);
        Entity.PlaceOwnerId = new PlaceOwnerId(domainEvent.PlaceOwnerId);
        Entity.EmailAddress = new PlaceOwnerEmailAddress(domainEvent.EmailAddress);
    }
}