using System;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Command.ActivateAccount;

public record ActivateAccountCommand(Guid AccountId): IRequest<ActivateAccountResponse>;