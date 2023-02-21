using EasyRent.RentalAd.Domain.RentalAd;
using FluentValidation;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdPricePerDay;

public class UpdateRentalAdPricePerDayValidator : AbstractValidator<UpdateRentalAdPricePerDayCommand>
{
    public UpdateRentalAdPricePerDayValidator()
    {
        RuleFor(x => x.NewPricePerDay)
            .NotNull()
            .InclusiveBetween(RentalAdValidationRules.PricePerDay.MinPrice,
                RentalAdValidationRules.PricePerDay.MaxPrice);
    }
}