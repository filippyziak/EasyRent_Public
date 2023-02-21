using EasyRent.Identity.Domain.Account.ValueObjects;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Command.RegisterAccount;

public record RegisterAccountCommand(
    string EmailAddress,
    string Password,
    AccountTypeEnum AccountType) : IRequest<RegisterAccountResponse>;