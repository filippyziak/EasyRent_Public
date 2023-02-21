using FluentValidation;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdPlaceOwner;

public class UpdateRentalAdPlaceOwnerValidator : AbstractValidator<UpdateRentalAdPlaceOwnerCommand>
{
    public UpdateRentalAdPlaceOwnerValidator()
    {
        RuleFor(x => x.NewEmailAddress)
            .NotNull();
    }
}