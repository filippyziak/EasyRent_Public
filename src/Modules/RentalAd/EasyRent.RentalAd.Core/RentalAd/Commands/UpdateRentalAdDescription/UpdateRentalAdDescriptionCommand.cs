using System;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdDescription;

public record UpdateRentalAdDescriptionCommand : IRequest<UpdateRentalAdDescriptionResponse>
{
    public Guid CurrentOwnerId { get; init; }
    public Guid RentalAdId { get; init; }
    public string NewDescription { get; init; }
}