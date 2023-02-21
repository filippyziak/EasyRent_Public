using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public sealed class RentalAdPlaceAddressCountryUpdatedEventHandler : DomainEventHandler<RentalAd, RentalAdPlaceAddressCountryUpdated>
{
    public RentalAdPlaceAddressCountryUpdatedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceAddressCountryUpdated domainEvent)
    {
        Entity.ApplyToEntity(Entity.PlaceAddress, domainEvent);
    }
}

public sealed class PlaceAddressRentalAdCountryUpdatedEventHandler : DomainEventHandler<PlaceAddress, RentalAdPlaceAddressCountryUpdated>
{
    public PlaceAddressRentalAdCountryUpdatedEventHandler(PlaceAddress entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceAddressCountryUpdated domainEvent)
    {
        Entity.Country = PlaceAddressCountry.FromString(domainEvent.NewCountry);
        Entity.City = PlaceAddressCity.FromString(domainEvent.NewCity);
        Entity.Street = PlaceAddressStreet.FromString(domainEvent.NewStreet);
    }
}