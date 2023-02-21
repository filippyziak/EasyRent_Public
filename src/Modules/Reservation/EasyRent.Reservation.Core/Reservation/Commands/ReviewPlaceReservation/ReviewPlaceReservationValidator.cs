using EasyRent.Reservation.Domain.PlaceReservation;
using FluentValidation;

namespace EasyRent.Reservation.Core.Reservation.Commands.ReviewPlaceReservation;

public class ReviewPlaceReservationValidator : AbstractValidator<ReviewPlaceReservationCommand>
{
    public ReviewPlaceReservationValidator()
    {
        RuleFor(x => x.ReviewDescription)
            .Length(PlaceReservationValidationRules.ReservationReview.MinLength,
                PlaceReservationValidationRules.ReservationReview.MaxLength)
            .When(x => x.ReviewDescription is not null);

        RuleFor(x => x.ReviewScore)
            .InclusiveBetween(PlaceReservationValidationRules.Score.MinAverageScore,
                PlaceReservationValidationRules.Score.MaxAverageScore);
    }
}