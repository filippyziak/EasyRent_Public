using EasyRent.EventSourcing;
using EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;
using EasyRent.Reservation.Domain.PlaceReservation.States;

namespace EasyRent.Reservation.Domain.PlaceReservation.Handlers;

public class PlaceReservationPaidEventHandler : DomainEventHandler<PlaceReservation, PlaceReservationPaid>
{
    public PlaceReservationPaidEventHandler(PlaceReservation entity) : base(entity)
    {
    }

    public override void Handle(PlaceReservationPaid domainEvent)
    {
        Entity.State = PlaceReservationState.Paid;
    }
}