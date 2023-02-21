using System;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdPlaceOwner;

public record UpdateRentalAdPlaceOwnerCommand : IRequest<UpdateRentalAdPlaceOwnerResponse>
{
    public Guid PlaceOwnerId { get; init; }
    public string NewEmailAddress { get; init; }
}