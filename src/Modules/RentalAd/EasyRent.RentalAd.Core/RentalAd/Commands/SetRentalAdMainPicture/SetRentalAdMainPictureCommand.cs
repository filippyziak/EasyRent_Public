using System;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.SetRentalAdMainPicture;

public record SetRentalAdMainPictureCommand : IRequest<SetRentalAdMainPictureResponse>
{
    public Guid CurrentOwnerId { get; init; }
    public Guid RentalAdId { get; init; }
    public Guid RentalAdMainPictureId { get; init; }
}