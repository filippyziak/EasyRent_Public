using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record PlaceOwnerEmailAddress : ValueObject<string>
{
    internal PlaceOwnerEmailAddress(string email) => Value = email;

    public static PlaceOwnerEmailAddress FromString(string email)
    {
        Validator.Text.NotNullOrEmpty(email);
        Validator.Text.ValidateEmail(email);

        return new PlaceOwnerEmailAddress(email);
    }
}