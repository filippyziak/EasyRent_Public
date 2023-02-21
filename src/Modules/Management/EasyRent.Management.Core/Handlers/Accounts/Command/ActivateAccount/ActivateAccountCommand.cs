using System;
using MediatR;

namespace EasyRent.Management.Core.Handlers.Accounts.Command.ActivateAccount;

public record ActivateAccountCommand : IRequest<ActivateAccountResponse>
{
    public Guid AccountId { get; init; }
}