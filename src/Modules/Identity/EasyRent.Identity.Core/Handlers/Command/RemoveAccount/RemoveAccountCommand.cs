using System;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Command.RemoveAccount;

public record RemoveAccountCommand(Guid AccountId) : IRequest<RemoveAccountResponse>;