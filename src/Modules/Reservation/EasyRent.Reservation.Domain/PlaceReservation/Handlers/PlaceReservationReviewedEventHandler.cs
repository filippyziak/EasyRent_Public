using EasyRent.EventSourcing;
using EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;
using EasyRent.Reservation.Domain.PlaceReservation.States;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects;

namespace EasyRent.Reservation.Domain.PlaceReservation.Handlers;

public class PlaceReservationReviewedEventHandler : DomainEventHandler<PlaceReservation, PlaceReservationReviewed>
{
    public PlaceReservationReviewedEventHandler(PlaceReservation entity) : base(entity)
    {
    }

    public override void Handle(PlaceReservationReviewed domainEvent)
    {
        Entity.State = PlaceReservationState.Reviewed;
        Entity.ReviewDescription = PlaceReservationReviewDescription.FromString(domainEvent.ReviewDescription);
        Entity.ReviewScore = PlaceReservationReviewScore.FromInt(domainEvent.ReviewScore);
    }
}