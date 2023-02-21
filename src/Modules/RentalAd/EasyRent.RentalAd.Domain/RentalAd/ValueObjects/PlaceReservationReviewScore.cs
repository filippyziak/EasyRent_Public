using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record PlaceReservationReviewScore : ValueObject<int>
{
    internal PlaceReservationReviewScore(int score) => Value = score;

    public static PlaceReservationReviewScore FromInt(int score)
    {
        Validator.Number.InRange(score,
            RentalAdValidationRules.Score.MinAverageScore,
            RentalAdValidationRules.Score.MaxAverageScore);

        return new PlaceReservationReviewScore(score);
    }
}