using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public sealed class RentalAdPlaceAddressCreatedEventHandler : DomainEventHandler<RentalAd, RentalAdPlaceAddressCreated>
{
    public RentalAdPlaceAddressCreatedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceAddressCreated domainEvent)
    {
        var placeAddress = new PlaceAddress(Entity.Apply);
        Entity.ApplyToEntity(placeAddress, domainEvent);
        Entity.PlaceAddress = placeAddress;
    }
}

public sealed class PlaceAddressRentalAdCreatedEventHandler : DomainEventHandler<PlaceAddress, RentalAdPlaceAddressCreated>
{
    public PlaceAddressRentalAdCreatedEventHandler(PlaceAddress entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceAddressCreated domainEvent)
    {
        Entity.RentalAdId = new RentalAdId(domainEvent.RentalAdId);
        Entity.PlaceAddressId = new PlaceAddressId(domainEvent.PlaceAddressId);
        Entity.Country = new PlaceAddressCountry(domainEvent.Country);
        Entity.City = new PlaceAddressCity(domainEvent.City);
        Entity.Street = new PlaceAddressStreet(domainEvent.Street);
    }
}