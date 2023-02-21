using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.DocumentStore;
using EasyRent.Infrastructure.Extensions;
using EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForRentalAd;
using EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForTenant;
using EasyRent.Reservation.Domain.PlaceReservation.States;
using EasyRent.Reservation.Infrastructure.DocumentStore.Documents;
using EasyRent.Reservation.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.Shared.Pagination;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EasyRent.Reservation.Infrastructure.Repositories.DocumentStore;

public class PlaceReservationDocumentRepository : MongoDbDocumentRepository<PlaceReservationDocument>, IPlaceReservationDocumentRepository
{
    public PlaceReservationDocumentRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
    {
    }

    public async Task<IPagedList<PlaceReservationDocument>> GetPlaceReservationsByTenantId(GetReservationsForTenantQuery query, CancellationToken cancellationToken)
        => await Collection.AsQueryable()
            .Where(pr => pr.TenantId == query.TenantId)
            .ToPagedListAsync(query, cancellationToken);

    public async Task<IPagedList<PlaceReservationDocument>> GetPlaceReservationsByRentalAdId(GetReservationsForRentalAdQuery query, CancellationToken cancellationToken)
        => await Collection.AsQueryable()
            .Where(pr => pr.RentalAdId == query.RentalAdId)
            .ToPagedListAsync(query, cancellationToken);

    public async Task<IReadOnlyList<PlaceReservationDocument>> GetPlaceReservationsByRentalAdId(Guid rentalAdId, CancellationToken cancellationToken)
        => await Collection.AsQueryable()
            .Where(pr => pr.RentalAdId == rentalAdId)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Guid>> GetPlaceReservationIdsToFinsish(CancellationToken cancellationToken)
        => await Collection.AsQueryable()
            .Where(pr => pr.DepartureDate < DateTime.Today && pr.State != PlaceReservationState.Finished.ToString())
            .Select(pr => pr.PlaceReservationId)
            .ToListAsync(cancellationToken);
}