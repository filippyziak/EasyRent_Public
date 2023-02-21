using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record RentalAdTitle : ValueObject<string>
{
    internal RentalAdTitle(string title) => Value = title;

    public static RentalAdTitle FromString(string title)
    {
        Validator.Text.NotNullOrEmpty(title);
        Validator.Text.InRange(
            title,
            RentalAdValidationRules.Title.MinLength,
            RentalAdValidationRules.Title.MaxLength);

        return new RentalAdTitle(title);
    }
}