using System;
using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

namespace EasyRent.RentalAd.Domain.RentalAd.Entities;

public class PlaceAddress : Entity<PlaceAddressId>
{
    public RentalAdId RentalAdId { get; internal set; }
    public PlaceAddressId PlaceAddressId { get; internal set; }
    public PlaceAddressCountry Country { get; internal set; }
    public PlaceAddressCity City { get; internal set; }
    public PlaceAddressStreet Street { get; internal set; }

    public PlaceAddress(Action<IDomainEvent> applier) : base(applier)
    {
    }

    public void UpdateCountry(PlaceAddressCountry country,
        PlaceAddressCity city,
        PlaceAddressStreet street)
        => Apply(new RentalAdPlaceAddressCountryUpdated(
            RentalAdId,
            PlaceAddressId,
            country,
            city,
            street));

    public void UpdateCity(PlaceAddressCity city,
        PlaceAddressStreet street)
        => Apply(new RentalAdPlaceAddressCityUpdated(
            RentalAdId,
            PlaceAddressId,
            city,
            street));


    public void UpdateStreet(PlaceAddressStreet street)
        => Apply(new RentalAdPlaceAddressStreetUpdated(
            RentalAdId,
            PlaceAddressId,
            street));
}