using System;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Command.UpdateAccountEmailAddress;

public record UpdateAccountEmailAddressCommand : IRequest<UpdateAccountEmailAddressResponse>
{
    public Guid AccountId { get; init; }
    public string NewEmailAddress { get; init; }
}