using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public class RentalAdPlaceOwnerEmailAddressUpdatedEventHandler : DomainEventHandler<RentalAd, RentalAdPlaceOwnerEmailAddressUpdated>
{
    public RentalAdPlaceOwnerEmailAddressUpdatedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceOwnerEmailAddressUpdated domainEvent)
    {
        Entity.ApplyToEntity(Entity.PlaceOwner, domainEvent);
    }
}

public class PlaceOwnerRentalAdEmailAddressUpdatedEventHandler : DomainEventHandler<PlaceOwner, RentalAdPlaceOwnerEmailAddressUpdated>
{
    public PlaceOwnerRentalAdEmailAddressUpdatedEventHandler(PlaceOwner entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceOwnerEmailAddressUpdated domainEvent)
    {
        Entity.EmailAddress = PlaceOwnerEmailAddress.FromString(domainEvent.NewEmailAddress);
    }
}