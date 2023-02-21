using System;
using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.States;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;

namespace EasyRent.RentalAd.Domain.RentalAd.Entities;

public class PlaceReservation : Entity<PlaceReservationId>
{
    public RentalAdId RentalAdId { get; internal set; }
    public PlaceReservationId PlaceReservationId { get; internal set; }
    public PlaceReservationPeriodDates PeriodDates { get; internal set; }
    public PlaceReservationReviewDescription ReviewDescription { get; internal set; }
    public PlaceReservationReviewScore ReviewScore { get; internal set; }
    public PlaceReservationState State { get; internal set; }

    public PlaceReservation(Action<IDomainEvent> applier) : base(applier)
    {
    }

    public void UpdateReview(PlaceReservationReviewDescription reviewDescription)
    {
        EnsureIsNotArchived();
        EnsureIsFinished();

        Apply(new RentalAdPlaceReservationReviewUpdated(
            RentalAdId,
            PlaceReservationId,
            reviewDescription));
    }

    public void UpdateReviewScore(PlaceReservationReviewScore reviewScore)
    {
        EnsureIsNotArchived();
        EnsureIsFinished();

        Apply(new RentalAdPlaceReservationReviewScoreUpdated(
            RentalAdId,
            PlaceReservationId,
            reviewScore));
    }

    public void Archive()
    {
        EnsureIsNotArchived();

        Apply(new RentalAdPlaceReservationArchived(
            RentalAdId,
            PlaceReservationId));
    }

    public void Finish()
    {
        EnsureIsNotArchived();
        EnsureIsFinished();

        if (PeriodDates.DepartureDate <= DateTime.Today)
        {
            Apply(new RentalAdPlaceReservationFinished(RentalAdId,
                PlaceReservationId));
        }

        throw new InvalidEntityStateException(this, "The reservation is still ongoing");
    }

    private void EnsureIsFinished()
    {
        if (State <= PlaceReservationState.Finished)
        {
            throw new InvalidEntityStateException(this, "Reservation is not finished yet");
        }
    }

    private void EnsureIsNotArchived()
    {
        if (State == PlaceReservationState.Archived)
        {
            throw new InvalidEntityStateException(this, "Reservation is archived");
        }
    }
}