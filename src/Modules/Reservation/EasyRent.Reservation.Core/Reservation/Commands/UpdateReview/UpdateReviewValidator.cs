using EasyRent.Reservation.Domain.PlaceReservation;
using FluentValidation;

namespace EasyRent.Reservation.Core.Reservation.Commands.UpdateReview;

public class UpdateReviewValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewValidator()
    {
        RuleFor(x => x.NewReviewDescription)
            .Length(PlaceReservationValidationRules.ReservationReview.MinLength,
                PlaceReservationValidationRules.ReservationReview.MaxLength)
            .When(x => x.NewReviewDescription is not null);

        RuleFor(x => x.NewReviewScore)
            .InclusiveBetween(PlaceReservationValidationRules.Score.MinAverageScore,
                PlaceReservationValidationRules.Score.MinAverageScore)
            .When(x => x.NewReviewScore is not null);
    }
}