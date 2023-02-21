using System;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.RemoveRentalAdPicture;

public record RemoveRentalAdPictureCommand : IRequest<RemoveRentalAdPictureResponse>
{
    public Guid CurrentOwnerId { get; init; }
    public Guid RentalAdId { get; init; }  
    public Guid PictureId { get; init; }  
}