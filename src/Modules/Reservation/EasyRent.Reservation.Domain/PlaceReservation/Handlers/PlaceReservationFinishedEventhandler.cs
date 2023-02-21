using EasyRent.EventSourcing;
using EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;
using EasyRent.Reservation.Domain.PlaceReservation.States;

namespace EasyRent.Reservation.Domain.PlaceReservation.Handlers;

public class PlaceReservationFinishedEventhandler : DomainEventHandler<PlaceReservation, PlaceReservationFinished>
{
    public PlaceReservationFinishedEventhandler(PlaceReservation entity) : base(entity)
    {
    }

    public override void Handle(PlaceReservationFinished domainEvent)
    {
        Entity.State = PlaceReservationState.Finished;
    }
}