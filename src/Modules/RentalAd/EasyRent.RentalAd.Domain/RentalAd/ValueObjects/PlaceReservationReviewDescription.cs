using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record PlaceReservationReviewDescription : ValueObject<string>
{
    internal PlaceReservationReviewDescription(string review) => Value = review;

    public static PlaceReservationReviewDescription FromString(string review)
    {
        Validator.Text.NotNullOrEmpty(review);
        Validator.Text.InRange(review,
            RentalAdValidationRules.ReservationReview.MinLength,
            RentalAdValidationRules.ReservationReview.MaxLength);

        return new PlaceReservationReviewDescription(review);
    }
}