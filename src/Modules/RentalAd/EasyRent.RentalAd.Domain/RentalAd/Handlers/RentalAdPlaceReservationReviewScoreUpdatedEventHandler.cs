using System.Linq;
using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.States;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public class RentalAdPlaceReservationReviewScoreUpdatedEventHandler : DomainEventHandler<RentalAd, RentalAdPlaceReservationReviewScoreUpdated>
{
    public RentalAdPlaceReservationReviewScoreUpdatedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceReservationReviewScoreUpdated domainEvent)
    {
        var averageScores = Entity.PlaceReservations
            .Where(r
                => r.State == PlaceReservationState.Reviewed)
            .Select(r
                => r.ReviewScore.Value).ToList();
        
        if (averageScores.Any())
        {
            Entity.AverageReviewScore = RentalAdAverageReviewScore.FromDouble(averageScores.Average());
        }
    }
}

public class PlaceReservationRentalAdReviewScoreUpdatedEventHandler : DomainEventHandler<PlaceReservation, RentalAdPlaceReservationReviewScoreUpdated>
{
    public PlaceReservationRentalAdReviewScoreUpdatedEventHandler(PlaceReservation entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceReservationReviewScoreUpdated domainEvent)
    {
        Entity.ReviewScore = new PlaceReservationReviewScore(domainEvent.reviewScore);
    }
}