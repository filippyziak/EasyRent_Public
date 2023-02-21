using EasyRent.RentalAd.ReadModels.ReadModels;
using EasyRent.Shared.Models;
using EasyRent.Shared.Pagination;

namespace EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAds;

public record GetRentalAdsResponse(Error Error = null) : BaseResponse(Error)
{
    public IPagedList<RentalAdReadModel> RentalAds { get; init; } = PagedList<RentalAdReadModel>.Empty;
    public int TotalPages { get; init; }
}