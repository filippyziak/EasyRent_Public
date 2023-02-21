using System;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Command.SuspendAccount;

public record SuspendAccountCommand(Guid AccountId) : IRequest<SuspendAccountResponse>;