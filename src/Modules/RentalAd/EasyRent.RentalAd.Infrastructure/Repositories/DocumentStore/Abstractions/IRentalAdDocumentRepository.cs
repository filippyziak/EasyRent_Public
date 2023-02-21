using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.DocumentStore;
using EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAds;
using EasyRent.RentalAd.Infrastructure.DocumentStore.Documents;
using EasyRent.Shared.Pagination;

namespace EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

public interface IRentalAdDocumentRepository : IDocumentRepository<RentalAdDocument>
{
    Task<IReadOnlyList<string>> GetRentalAdIdsByPlaceOwnerIdAsync(Guid placeOwnerId, CancellationToken cancellationToken = default);
    Task<IPagedList<RentalAdDocument>> GetFilteredRentalAdsAsync(GetRentalAdsQuery query, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RentalAdDocument>> GetRentalAdsForPlaceOwnerAsync(Guid placeOwnerId, CancellationToken cancellationToken);
}