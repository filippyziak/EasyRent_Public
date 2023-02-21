using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public sealed class RentalAdPlaceAddressStreetUpdatedEventHandler : DomainEventHandler<RentalAd, RentalAdPlaceAddressStreetUpdated>
{
    public RentalAdPlaceAddressStreetUpdatedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceAddressStreetUpdated domainEvent)
    {
        Entity.ApplyToEntity(Entity.PlaceAddress, domainEvent);
    }
}

public sealed class PlaceAddressRentalAdStreetUpdatedEventHandler : DomainEventHandler<PlaceAddress, RentalAdPlaceAddressStreetUpdated>
{
    public PlaceAddressRentalAdStreetUpdatedEventHandler(PlaceAddress entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceAddressStreetUpdated domainEvent)
    {
        Entity.Street = PlaceAddressStreet.FromString(domainEvent.NewStreet);
    }
}