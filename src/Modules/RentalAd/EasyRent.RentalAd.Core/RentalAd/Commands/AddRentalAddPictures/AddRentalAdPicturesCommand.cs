using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.AddRentalAddPictures;

public record AddRentalAdPicturesCommand : IRequest<AddRentalAdPicturesResponse>
{
    public Guid CurrentOwnerId { get; init; }
    public Guid RentalAdId { get; init; }
    public IReadOnlyList<IFormFile> PictureFiles { get; init; } = ImmutableList<IFormFile>.Empty;
}