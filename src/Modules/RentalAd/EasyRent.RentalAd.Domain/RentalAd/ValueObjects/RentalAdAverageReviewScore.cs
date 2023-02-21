using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record RentalAdAverageReviewScore : ValueObject<double>
{
    internal RentalAdAverageReviewScore(double averageScore) => Value = averageScore;

    public static RentalAdAverageReviewScore FromDouble(double averageScore)
    {
        Validator.Number.InRange(
            averageScore,
            RentalAdValidationRules.Score.MinAverageScore,
            RentalAdValidationRules.Score.MinAverageScore);

        return new RentalAdAverageReviewScore(averageScore);
    }
}