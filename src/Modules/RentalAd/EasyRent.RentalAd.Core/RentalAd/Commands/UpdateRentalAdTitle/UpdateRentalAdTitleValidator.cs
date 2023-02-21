using EasyRent.RentalAd.Domain.RentalAd;
using FluentValidation;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdTitle;

public class UpdateRentalAdTitleValidator : AbstractValidator<UpdateRentalAdTitleCommand>
{
    public UpdateRentalAdTitleValidator()
    {
        RuleFor(x => x.NewTitle)
            .NotNull()
            .MinimumLength(RentalAdValidationRules.Title.MinLength)
            .MaximumLength(RentalAdValidationRules.Title.MaxLength);
    }
}