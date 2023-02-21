using EasyRent.RentalAd.Domain.RentalAd;
using FluentValidation;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdReservation;

public class UpdateRentalAdReservationValidator : AbstractValidator<UpdateRentalAdReservationCommand>
{
    public UpdateRentalAdReservationValidator()
    {
        RuleFor(x => x.ReviewDescription)
            .NotNull()
            .Length(RentalAdValidationRules.ReservationReview.MinLength,
                RentalAdValidationRules.ReservationReview.MaxLength);
        RuleFor(x => x.ReviewScore)
            .InclusiveBetween(RentalAdValidationRules.Score.MinScore,
                RentalAdValidationRules.Score.MaxScore);
    }
}