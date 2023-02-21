using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record RentalAdPricePerDay : ValueObject<decimal>
{
    internal RentalAdPricePerDay(decimal amount) => Value = amount;

    public static RentalAdPricePerDay FromDecimal(decimal amount)
    {
        Validator.Number.InRange(
            amount,
            RentalAdValidationRules.PricePerDay.MinPrice,
            RentalAdValidationRules.PricePerDay.MaxPrice);

        return new RentalAdPricePerDay(amount);
    }
}