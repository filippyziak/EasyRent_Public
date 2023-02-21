using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record PlaceAddressStreet : ValueObject<string>
{
    internal PlaceAddressStreet(string street) => Value = street;

    public static PlaceAddressStreet FromString(string street)
    {
        Validator.Text.NotNullOrEmpty(street);
        Validator.Text.InRange(street,
            RentalAdValidationRules.PlaceAddress.MinLength,
            RentalAdValidationRules.PlaceAddress.MaxLength);

        return new PlaceAddressStreet(street);
    }
}