using EasyRent.RentalAd.Domain.RentalAd;
using FluentValidation;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdAddress;

public class UpdateRentalAdAddressValidator : AbstractValidator<UpdateRentalAdAddressCommand>
{
    public UpdateRentalAdAddressValidator()
    {
        RuleFor(x => x.NewCountry)
            .NotNull()
            .Length(RentalAdValidationRules.PlaceAddress.MinLength,
                RentalAdValidationRules.PlaceAddress.MaxLength);

        RuleFor(x => x.NewCity)
            .NotNull()
            .Length(RentalAdValidationRules.PlaceAddress.MinLength,
                RentalAdValidationRules.PlaceAddress.MaxLength);

        RuleFor(x => x.NewCity)
            .NotNull()
            .Length(RentalAdValidationRules.PlaceAddress.MinLength,
                RentalAdValidationRules.PlaceAddress.MaxLength);
    }
}