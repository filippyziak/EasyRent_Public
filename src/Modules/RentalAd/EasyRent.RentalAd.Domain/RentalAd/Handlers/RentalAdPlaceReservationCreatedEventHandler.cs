using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public class RentalAdPlaceReservationCreatedEventHandler : DomainEventHandler<RentalAd, RentalAdPlaceReservationCreated>
{
    public RentalAdPlaceReservationCreatedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceReservationCreated domainEvent)
    {
        var placeReservation = new PlaceReservation(Entity.Apply);
        Entity.ApplyToEntity(placeReservation, domainEvent);
        Entity.PlaceReservations.Add(placeReservation);
    }
}

public class PlaceReservationRentalAdCreatedEventHandler : DomainEventHandler<PlaceReservation, RentalAdPlaceReservationCreated>
{
    public PlaceReservationRentalAdCreatedEventHandler(PlaceReservation entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceReservationCreated domainEvent)
    {
        Entity.RentalAdId = new RentalAdId(domainEvent.RentalAdId);
        Entity.PlaceReservationId = new PlaceReservationId(domainEvent.PlaceReservationId);
        Entity.PeriodDates = new PlaceReservationPeriodDates(domainEvent.ArrivalDate, domainEvent.DepartureDate);
    }
}