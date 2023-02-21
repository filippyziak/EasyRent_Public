using System;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdAddress;

public record UpdateRentalAdAddressCommand : IRequest<UpdateRentalAdAddressResponse>
{
    public Guid CurrentOwnerId { get; init; }
    public Guid RentalAdId { get; init; }
    public string NewCountry { get; init; }
    public string NewCity { get; init; }
    public string NewStreet { get; init; }
}