using System;
using EasyRent.Management.Domain.Accounts.ValueObjects;
using MediatR;

namespace EasyRent.Management.Core.Handlers.Accounts.Command.CreateAccount;

public record CreateAccountCommand : IRequest<CreateAccountResponse>
{
    public Guid AccountId { get; init; }
    public AccountTypeEnum Type { get; init; }
}