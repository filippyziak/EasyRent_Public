using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public class RentalAdPlaceReservationReviewUpdatedEventHandler : DomainEventHandler<PlaceReservation, RentalAdPlaceReservationReviewUpdated>
{
    public RentalAdPlaceReservationReviewUpdatedEventHandler(PlaceReservation entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceReservationReviewUpdated domainEvent)
    {
        Entity.ReviewDescription = PlaceReservationReviewDescription.FromString(domainEvent.review);
    }
}