using System;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Command.UpdateAccountPassword;

public record UpdateAccountPasswordCommand : IRequest<UpdateAccountPasswordResponse>
{
    public Guid AccountId { get; init; }
    public string NewPassword { get; init; }
}