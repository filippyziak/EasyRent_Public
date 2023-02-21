using EasyRent.Identity.Domain.Account.ValueObjects;

namespace EasyRent.Identity.Requests;

public record RegisterAccountRequest(
    string EmailAddress,
    string Password,
    AccountTypeEnum AccountType);