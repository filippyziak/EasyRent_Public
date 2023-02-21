using System;
using MediatR;

namespace EasyRent.Management.Core.Handlers.Accounts.Command.SuspendAccount;

public record SuspendAccountCommand : IRequest<SuspendAccountResponse>
{
    public Guid AccountId { get; init; }
    public Guid CurrentAccountId { get; init; }
}