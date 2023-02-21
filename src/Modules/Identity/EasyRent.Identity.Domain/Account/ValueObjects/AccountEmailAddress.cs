using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.Identity.Domain.Account.ValueObjects;

public record AccountEmailAddress : ValueObject<string>
{
    internal AccountEmailAddress(string email) => Value = email;

    public static AccountEmailAddress FromString(string email)
    {
        Validator.Text.NotNullOrEmpty(email);
        Validator.Text.ValidateEmail(email);

        return new AccountEmailAddress(email);
    }
}