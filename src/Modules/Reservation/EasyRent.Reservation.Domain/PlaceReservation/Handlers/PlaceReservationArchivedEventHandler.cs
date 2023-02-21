using EasyRent.EventSourcing;
using EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;
using EasyRent.Reservation.Domain.PlaceReservation.States;

namespace EasyRent.Reservation.Domain.PlaceReservation.Handlers;

public class PlaceReservationArchivedEventHandler : DomainEventHandler<PlaceReservation, PlaceReservationArchived>
{
    public PlaceReservationArchivedEventHandler(PlaceReservation entity) : base(entity)
    {
    }

    public override void Handle(PlaceReservationArchived domainEvent)
    {
        Entity.State = PlaceReservationState.Archived;
    }
}