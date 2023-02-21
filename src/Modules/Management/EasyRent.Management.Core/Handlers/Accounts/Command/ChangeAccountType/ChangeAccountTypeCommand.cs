using System;
using EasyRent.Management.Domain.Accounts.ValueObjects;
using MediatR;

namespace EasyRent.Management.Core.Handlers.Accounts.Command.ChangeAccountType;

public record ChangeAccountTypeCommand : IRequest<ChangeAccountTypeResponse>
{
    public Guid AccountId { get; init; }
    public AccountTypeEnum AccountType { get; init; }
}