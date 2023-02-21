using EasyRent.EventSourcing;
using EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects;

namespace EasyRent.Reservation.Domain.PlaceReservation.Handlers;

public class PlaceReservationReviewScoreUpdatedEventHandler : DomainEventHandler<PlaceReservation, PlaceReservationReviewScoreUpdated>
{
    public PlaceReservationReviewScoreUpdatedEventHandler(PlaceReservation entity) : base(entity)
    {
    }

    public override void Handle(PlaceReservationReviewScoreUpdated domainEvent)
    {
        Entity.ReviewScore = PlaceReservationReviewScore.FromInt(domainEvent.ReviewScore);
    }
}