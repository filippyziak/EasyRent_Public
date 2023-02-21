using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAds;
using EasyRent.RentalAd.ReadModels.ReadModels;
using EasyRent.Shared.Pagination;

namespace EasyRent.RentalAd.Core.Abstractions;

public interface IRentalAdReadOnlyRepository
{
    Task<IReadOnlyList<string>> GetRentalAdIdsByPlaceOwnerIdAsync(Guid placeOwnerId, CancellationToken cancellationToken = default);
    Task<RentalAdReadModel> GetRentalAdByIdAsync(Guid rentalAdId, CancellationToken cancellationToken = default);
    Task<IPagedList<RentalAdReadModel>> GetRentalAdsAsync(GetRentalAdsQuery query, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RentalAdReadModel>> GetRentalAdsForPlaceOwnerAsync(Guid placeOwnerId, CancellationToken cancellationToken = default);
}