using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.Management.Domain.PlaceFeatures.ValueObjects;

public record PlaceFeaturePictureUrl : ValueObject<string>
{
    internal PlaceFeaturePictureUrl(string url) => Value = url;

    public static PlaceFeaturePictureUrl FromString(string url)
    {
        Validator.Text.NotNullOrEmpty(url);
        Validator.Text.ValidateUrl(url);

        return new PlaceFeaturePictureUrl(url);
    }
}