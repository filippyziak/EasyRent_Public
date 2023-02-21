using EasyRent.EventSourcing;
using EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;
using EasyRent.Reservation.Domain.PlaceReservation.States;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;

namespace EasyRent.Reservation.Domain.PlaceReservation;

public class PlaceReservation : AggregateRoot<PlaceReservationId>
{
    public PlaceReservationId Id { get; internal set; }
    public PlaceReservationPeriodDates PeriodDates { get; internal set; }
    public PlaceReservationReviewDescription ReviewDescription { get; internal set; }
    public PlaceReservationReviewScore ReviewScore { get; internal set; }

    public TenantId TenantId { get; internal set; }
    public OwnerId OwnerId { get; internal set; }
    public RentalAdId RentalAdId { get; internal set; }

    public PlaceReservationState State { get; internal set; }

    protected PlaceReservation()
    {
    }

    public PlaceReservation(PlaceReservationId Id,
        PlaceReservationPeriodDates periodDates,
        TenantId tenantId,
        OwnerId ownerId,
        RentalAdId rentalAdId)
        => Apply(new PlaceReservationCreated(Id,
            periodDates.ArrivalDate,
            periodDates.DepartureDate,
            tenantId,
            ownerId,
            rentalAdId));

    public void ReviewReservation(PlaceReservationReviewDescription reviewDescription,
        PlaceReservationReviewScore reviewScore)
    {
        if (State != PlaceReservationState.Finished)
        {
            throw new InvalidEntityStateException(this, "Reservation cannot be reviewed");
        }

        Apply(new PlaceReservationReviewed(Id,
            reviewDescription,
            reviewScore));
    }

    public void UpdateReviewDescription(PlaceReservationReviewDescription reviewDescription)
    {
        if (State != PlaceReservationState.Reviewed)
        {
            throw new InvalidEntityStateException(this, "Reservation is not yet reviewed");
        }

        Apply(new PlaceReservationReviewDescriptionUpdated(Id, reviewDescription));
    }

    public void UpdateReviewScore(PlaceReservationReviewScore reviewScore)
    {
        if (State != PlaceReservationState.Reviewed)
        {
            throw new InvalidEntityStateException(this, "Reservation is not yet reviewed");
        }

        Apply(new PlaceReservationReviewScoreUpdated(Id, reviewScore));
    }

    public void Pay()
    {
        EnsureNotArchived();
        Apply(new PlaceReservationPaid(Id));
    }

    public void Finish()
    {
        EnsureNotArchived();
        Apply(new PlaceReservationFinished(Id));
    }

    public void Cancel()
    {
        EnsureOngoing();
        Apply(new PlaceReservationArchived(Id));
    }

    public void Archive()
    {
        EnsureNotArchived();
        Apply(new PlaceReservationArchived(Id));
    }

    protected override void EnsureValidState()
    {
        var valid = Id is not null
                    && TenantId is not null
                    && RentalAdId is not null;

        if (State == PlaceReservationState.Reviewed)
        {
            valid &= ReviewDescription is not null
                     && ReviewScore is not null;
        }

        if (!valid)
        {
            throw new InvalidEntityStateException(this, "Reservation validation failed");
        }
    }

    private void EnsureNotArchived()
    {
        if (State == PlaceReservationState.Archived)
        {
            throw new InvalidEntityStateException(this, "Reservation is archived");
        }
    }
    
    private void EnsureOngoing()
    {
        if (State != PlaceReservationState.Ongoing)
        {
            throw new InvalidEntityStateException(this, "Reservation is not ongoing");
        }
    }
}