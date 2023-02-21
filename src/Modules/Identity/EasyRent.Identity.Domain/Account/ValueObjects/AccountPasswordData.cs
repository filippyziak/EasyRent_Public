using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.DomainServices;
using EasyRent.Shared;

namespace EasyRent.Identity.Domain.Account.ValueObjects;

public record AccountPasswordData : ValueObject
{
    public byte[] PasswordHash { get; }
    public string PasswordSalt { get; }

    internal AccountPasswordData(byte[] passwordHash, string passwordSalt)
    {
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    public static AccountPasswordData FromString(string password, IHashProvider hashProvider)
    {
        Validator.Text.NotNullOrEmpty(password);
        Validator.Text.InRange(password, 1, 64);

        var passwordSalt = hashProvider.CreateSalt();
        var passwordHash = hashProvider.CreateHash(password, passwordSalt);

        return new AccountPasswordData(passwordHash, passwordSalt);
    }

    public static AccountPasswordData FromStringWithSalt(string password, string passwordSalt, IHashProvider hashProvider)
    {
        Validator.Text.NotNullOrEmpty(password);
        Validator.Text.InRange(password, 1, 64);

        var passwordHash = hashProvider.CreateHash(password, passwordSalt);

        return new AccountPasswordData(passwordHash, passwordSalt);
    }
}