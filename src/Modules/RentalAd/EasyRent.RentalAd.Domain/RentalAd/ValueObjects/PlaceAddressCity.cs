using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record PlaceAddressCity : ValueObject<string>
{
    internal PlaceAddressCity(string city) => Value = city;

    public static PlaceAddressCity FromString(string city)
    {
        Validator.Text.NotNullOrEmpty(city);
        Validator.Text.CotainsOnlyLetters(city);
        Validator.Text.InRange(city,
            RentalAdValidationRules.PlaceAddress.MinLength,
            RentalAdValidationRules.PlaceAddress.MaxLength);

        return new PlaceAddressCity(city);
    }
}