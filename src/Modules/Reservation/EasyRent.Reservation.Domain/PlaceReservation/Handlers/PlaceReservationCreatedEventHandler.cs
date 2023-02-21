using EasyRent.EventSourcing;
using EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects.Ids;

namespace EasyRent.Reservation.Domain.PlaceReservation.Handlers;

public class PlaceReservationCreatedEventHandler : DomainEventHandler<PlaceReservation, PlaceReservationCreated>
{
    public PlaceReservationCreatedEventHandler(PlaceReservation entity) : base(entity)
    {
    }

    public override void Handle(PlaceReservationCreated domainEvent)
    {
        Entity.Id = new PlaceReservationId(domainEvent.PlaceReservationId);
        Entity.PeriodDates = new PlaceReservationPeriodDates(domainEvent.ArrivalDate, domainEvent.DepartureDate);
        Entity.TenantId = new TenantId(domainEvent.TenantId);
        Entity.OwnerId = new OwnerId(domainEvent.OwnerId);
        Entity.RentalAdId = new RentalAdId(domainEvent.RentalAdId);
    }
}