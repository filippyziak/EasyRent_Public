using System;
using MediatR;

namespace EasyRent.Management.Core.Handlers.Accounts.Command.DeleteAccount;

public record DeleteAccountCommand : IRequest<DeleteAccountResponse>
{
    public Guid AccountId { get; init; }
}