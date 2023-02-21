using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record PlaceAddressCountry : ValueObject<string>
{
    internal PlaceAddressCountry(string country) => Value = country;

    public static PlaceAddressCountry FromString(string country)
    {
        Validator.Text.NotNullOrEmpty(country);
        Validator.Text.CotainsOnlyLetters(country);
        Validator.Text.InRange(country,
            RentalAdValidationRules.PlaceAddress.MinLength,
            RentalAdValidationRules.PlaceAddress.MaxLength);

        return new PlaceAddressCountry(country);
    }
}