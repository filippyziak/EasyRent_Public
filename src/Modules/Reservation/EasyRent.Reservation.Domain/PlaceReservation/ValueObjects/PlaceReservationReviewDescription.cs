using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.Reservation.Domain.PlaceReservation.ValueObjects;

public record PlaceReservationReviewDescription : ValueObject<string>
{
    internal PlaceReservationReviewDescription(string review) => Value = review;

    public static PlaceReservationReviewDescription FromString(string review)
    {
        Validator.Text.NotNullOrEmpty(review);
        Validator.Text.InRange(review,
            PlaceReservationValidationRules.ReservationReview.MinLength,
            PlaceReservationValidationRules.ReservationReview.MaxLength);

        return new PlaceReservationReviewDescription(review);
    }
}