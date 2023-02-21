using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record RentalAdDescription : ValueObject<string>
{
    internal RentalAdDescription(string description) => Value = description;

    public static RentalAdDescription FromString(string description)
    {
        Validator.Text.NotNullOrEmpty(description);
        Validator.Text.InRange(
            description,
            RentalAdValidationRules.Description.MinLength,
            RentalAdValidationRules.Description.MaxLength);

        return new RentalAdDescription(description);
    }
}