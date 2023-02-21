using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public sealed class RentalAdPlaceAddressCityUpdatedEventHandler : DomainEventHandler<RentalAd, RentalAdPlaceAddressCityUpdated>
{
    public RentalAdPlaceAddressCityUpdatedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceAddressCityUpdated domainEvent)
    {
        Entity.ApplyToEntity(Entity.PlaceAddress, domainEvent);
    }
}

public sealed class PlaceAddressRentalAdCityUpdatedEventHandler : DomainEventHandler<PlaceAddress, RentalAdPlaceAddressCityUpdated>
{
    public PlaceAddressRentalAdCityUpdatedEventHandler(PlaceAddress entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceAddressCityUpdated domainEvent)
    {
        Entity.City = PlaceAddressCity.FromString(domainEvent.NewCity);
        Entity.Street = PlaceAddressStreet.FromString(domainEvent.NewStreet);
    }
}