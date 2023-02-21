using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.DocumentStore;
using EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForRentalAd;
using EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForTenant;
using EasyRent.Reservation.Infrastructure.DocumentStore.Documents;
using EasyRent.Shared.Pagination;

namespace EasyRent.Reservation.Infrastructure.Repositories.DocumentStore.Abstractions;

public interface IPlaceReservationDocumentRepository : IDocumentRepository<PlaceReservationDocument>
{
    Task<IPagedList<PlaceReservationDocument>> GetPlaceReservationsByTenantId(GetReservationsForTenantQuery query, CancellationToken cancellationToken);
    Task<IPagedList<PlaceReservationDocument>> GetPlaceReservationsByRentalAdId(GetReservationsForRentalAdQuery query, CancellationToken cancellationToken);
    Task<IReadOnlyList<PlaceReservationDocument>> GetPlaceReservationsByRentalAdId(Guid rentalAdId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Guid>> GetPlaceReservationIdsToFinsish(CancellationToken cancellationToken);
}