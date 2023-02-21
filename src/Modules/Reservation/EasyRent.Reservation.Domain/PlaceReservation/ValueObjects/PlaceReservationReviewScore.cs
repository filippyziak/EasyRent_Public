using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.Reservation.Domain.PlaceReservation.ValueObjects;

public record PlaceReservationReviewScore : ValueObject<int>
{
    internal PlaceReservationReviewScore(int score) => Value = score;

    public static PlaceReservationReviewScore FromInt(int score)
    {
        Validator.Number.InRange(score,
            PlaceReservationValidationRules.Score.MinAverageScore,
            PlaceReservationValidationRules.Score.MaxAverageScore);

        return new PlaceReservationReviewScore(score);
    }
}