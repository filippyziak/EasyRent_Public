using EasyRent.RentalAd.Domain.RentalAd;
using FluentValidation;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdDescription;

public class UpdateRentalAdDescriptionValidator : AbstractValidator<UpdateRentalAdDescriptionCommand>
{
    public UpdateRentalAdDescriptionValidator()
    {
        RuleFor(x => x.NewDescription)
            .NotNull()
            .MinimumLength(RentalAdValidationRules.Description.MinLength)
            .MaximumLength(RentalAdValidationRules.Description.MaxLength);
    }
}