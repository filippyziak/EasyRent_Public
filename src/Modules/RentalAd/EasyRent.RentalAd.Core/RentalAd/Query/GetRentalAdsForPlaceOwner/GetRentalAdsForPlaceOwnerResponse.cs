using System.Collections.Generic;
using System.Collections.Immutable;
using EasyRent.RentalAd.ReadModels.ReadModels;
using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAdsForPlaceOwner;

public record GetRentalAdsForPlaceOwnerResponse(Error Error = null) : BaseResponse(Error)
{
    public IReadOnlyList<RentalAdReadModel> RentalAds { get; init; } = ImmutableList<RentalAdReadModel>.Empty;
  
}