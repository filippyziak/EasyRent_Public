using EasyRent.EventSourcing;
using EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects;

namespace EasyRent.Reservation.Domain.PlaceReservation.Handlers;

public class PlaceReservationReviewDescriptionUpdatedEventHandler : DomainEventHandler<PlaceReservation, PlaceReservationReviewDescriptionUpdated>
{
    public PlaceReservationReviewDescriptionUpdatedEventHandler(PlaceReservation entity) : base(entity)
    {
    }

    public override void Handle(PlaceReservationReviewDescriptionUpdated domainEvent)
    {
        Entity.ReviewDescription = PlaceReservationReviewDescription.FromString(domainEvent.ReviewDescription);
    }
}