using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.Management.Domain.PlaceFeatures.ValueObjects;

public record PlaceFeatureDescription : ValueObject<string>
{
    internal PlaceFeatureDescription(string description) => Value = description;

    public static PlaceFeatureDescription FromString(string description)
    {
        Validator.Text.NotNullOrEmpty(description);
        Validator.Text.InRange(description,
            ManagementValidationRules.PlaceFeature.Description.MinLength,
            ManagementValidationRules.PlaceFeature.Description.MaxLength);

        return new PlaceFeatureDescription(description);
    }
}