using EasyRent.RentalAd.Domain.RentalAd;
using FluentValidation;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.CreateRentalAd;

public class CreateRentalAdValidator : AbstractValidator<CreateRentalAdCommand>
{
    public CreateRentalAdValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .MinimumLength(RentalAdValidationRules.Title.MinLength)
            .MaximumLength(RentalAdValidationRules.Title.MaxLength);

        RuleFor(x => x.Description)
            .NotNull()
            .MinimumLength(RentalAdValidationRules.Description.MinLength)
            .MaximumLength(RentalAdValidationRules.Description.MaxLength);

        RuleFor(x => x.PricePerDay)
            .NotNull()
            .InclusiveBetween(RentalAdValidationRules.PricePerDay.MinPrice,
                RentalAdValidationRules.PricePerDay.MaxPrice);

        RuleFor(x => x.City)
            .NotNull()
            .MinimumLength(RentalAdValidationRules.PlaceAddress.MinLength)
            .MaximumLength(RentalAdValidationRules.PlaceAddress.MaxLength);

        RuleFor(x => x.Country)
            .NotNull()
            .MinimumLength(RentalAdValidationRules.PlaceAddress.MinLength)
            .MaximumLength(RentalAdValidationRules.PlaceAddress.MaxLength);

        RuleFor(x => x.Street)
            .NotNull()
            .MinimumLength(RentalAdValidationRules.PlaceAddress.MinLength)
            .MaximumLength(RentalAdValidationRules.PlaceAddress.MaxLength);
    }
}