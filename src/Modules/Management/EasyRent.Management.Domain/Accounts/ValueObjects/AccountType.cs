using System;
using EasyRent.EventSourcing;

namespace EasyRent.Management.Domain.Accounts.ValueObjects;

public record AccountType : ValueObject<AccountTypeEnum>
{
    private AccountType(AccountTypeEnum type) => Value = type;
    internal AccountType(string type) => Value = Enum.Parse<AccountTypeEnum>(type);

    public static AccountType FromEnum(AccountTypeEnum type) => new(type);

    public static AccountType FromString(string type) => new(type);

    public static implicit operator string(AccountType instance) => instance.Value.ToString();
}

public enum AccountTypeEnum
{
    Tenant,
    PlaceOwner,
    Moderator
}