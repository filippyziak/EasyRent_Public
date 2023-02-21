using System.Collections.Generic;
using System.Collections.Immutable;
using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.AddRentalAddPictures;

public record AddRentalAdPicturesResponse(Error Error = null) : BaseResponse(Error)
{
    public IReadOnlyList<string> PicturesUrls { get; init; } = ImmutableList<string>.Empty;
}