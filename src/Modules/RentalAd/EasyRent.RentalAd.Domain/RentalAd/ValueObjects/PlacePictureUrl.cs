using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record PlacePictureUrl : ValueObject<string>
{
    internal PlacePictureUrl(string photoUrl) => Value = photoUrl;

    public static PlacePictureUrl FromString(string photoUrl)
    {
        Validator.Text.ValidateUrl(photoUrl);

        return new PlacePictureUrl(photoUrl);
    }
}