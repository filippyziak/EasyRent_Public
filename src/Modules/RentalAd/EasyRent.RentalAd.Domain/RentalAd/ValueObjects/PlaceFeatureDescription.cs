using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record PlaceFeatureDescription : ValueObject<string>
{
    internal PlaceFeatureDescription(string description) => Value = description;

    public static PlaceFeatureDescription FromString(string description)
    {
        Validator.Text.NotNullOrEmpty(description);
        Validator.Text.InRange(description,
            RentalAdValidationRules.PlaceFeature.Description.MinLength,
            RentalAdValidationRules.PlaceFeature.Description.MaxLength);

        return new PlaceFeatureDescription(description);
    }
}