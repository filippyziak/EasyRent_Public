using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.DocumentStore;
using EasyRent.Infrastructure.Extensions;
using EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAds;
using EasyRent.RentalAd.Domain.RentalAd.States;
using EasyRent.RentalAd.Infrastructure.DocumentStore.Documents;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.Shared.Pagination;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore;

public class RentalAdDocumentRepository : MongoDbDocumentRepository<RentalAdDocument>, IRentalAdDocumentRepository
{
    public RentalAdDocumentRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
    {
    }

    public async Task<IReadOnlyList<string>> GetRentalAdIdsByPlaceOwnerIdAsync(Guid placeOwnerId, CancellationToken cancellationToken = default)
        => await Collection.AsQueryable()
            .Where(r => r.PlaceOwner.PlaceOwnerId == placeOwnerId)
            .Select(r => r.RentalAdId)
            .ToListAsync(cancellationToken);

    public async Task<IPagedList<RentalAdDocument>> GetFilteredRentalAdsAsync(GetRentalAdsQuery query, CancellationToken cancellationToken = default)
    {
        var collection = Collection.AsQueryable()
            .WhereIf(r => r.PlaceAddress.Country.ToLower().Contains(query.Country.ToLower()), query.Country is not null)
            .WhereIf(r => r.PlaceAddress.City.ToLower().Contains(query.City.ToLower()), query.City is not null)
            .WhereIf(r => r.PlaceAddress.Street.ToLower().Contains(query.Street.ToLower()), query.Street is not null)
            .Where(r => r.State != RentalAdState.Archived.ToString());

        collection = (query.ArrivalDate, query.DepartureDate) switch
        {
            { ArrivalDate: not null, DepartureDate: not null } => collection
                .Where(r => !r.PlaceReservations.Any(pr => (pr.ArrivalDate <= query.ArrivalDate
                                                           && pr.DepartureDate >= query.ArrivalDate)
                                                           || (pr.ArrivalDate <= query.DepartureDate
                                                           && pr.DepartureDate >= query.DepartureDate)
                                                           || (pr.ArrivalDate >= query.ArrivalDate
                                                               && pr.DepartureDate >= query.ArrivalDate
                                                               && pr.ArrivalDate <= query.DepartureDate
                                                               && pr.DepartureDate <= query.DepartureDate))),
            { ArrivalDate: not null, DepartureDate: null } => collection
                .Where(r => !r.PlaceReservations.Any(pr => (pr.ArrivalDate <= query.ArrivalDate
                                                           && pr.DepartureDate >= query.ArrivalDate)
                                                           || (pr.ArrivalDate <= query.DepartureDate
                                                           && pr.DepartureDate >= query.DepartureDate)
                                                           || (pr.ArrivalDate >= query.ArrivalDate
                                                               && pr.DepartureDate >= query.ArrivalDate
                                                               && pr.ArrivalDate <= query.DepartureDate
                                                               && pr.DepartureDate <= query.DepartureDate))),
            _ => collection
        };

        return await collection.ToPagedListAsync(query, cancellationToken);
    }

    public async Task<IReadOnlyList<RentalAdDocument>> GetRentalAdsForPlaceOwnerAsync(Guid placeOwnerId, CancellationToken cancellationToken)
        => await Collection.AsQueryable()
            .Where(r => r.PlaceOwner.PlaceOwnerId == placeOwnerId)
            .ToListAsync(cancellationToken);
}