using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record PlaceOwnerPictureUrl : ValueObject<string>
{
    internal PlaceOwnerPictureUrl(string pictureUrl) => Value = pictureUrl;

    public static PlaceOwnerPictureUrl FromString(string pictureUrl)
    {
        Validator.Text.NotNullOrEmpty(pictureUrl);
        Validator.Text.ValidateUrl(pictureUrl);

        return new PlaceOwnerPictureUrl(pictureUrl);
    }
}