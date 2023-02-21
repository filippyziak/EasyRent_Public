using System;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdTitle;

public record UpdateRentalAdTitleCommand : IRequest<UpdateRentalAdTitleResponse>
{
    public Guid CurrentOwnerId { get; init; }
    public Guid RentalAdId { get; init; }
    public string NewTitle { get; init; }
}