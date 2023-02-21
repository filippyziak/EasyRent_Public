using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForRentalAd;
using EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForTenant;
using EasyRent.Reservation.ReadModels.ReadModels;
using EasyRent.Shared.Pagination;

namespace EasyRent.Reservation.Core.Abstractions;

public interface IReservationReadOnlyRepository
{
    Task<IPagedList<PlaceReservationReadModel>> GetPlaceReservationsByTenantId(GetReservationsForTenantQuery query, CancellationToken cancellationToken = default);
    Task<IPagedList<PlaceReservationReadModel>> GetPlaceReservationsByRentalAdId(GetReservationsForRentalAdQuery query, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlaceReservationReadModel>> GetPlaceReservationsByRentalAdId(Guid rentalAdId, CancellationToken cancellationToken = default);
    Task<PlaceReservationReadModel> GetPlaceReservationById(Guid placeReservationId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Guid>> GetPlaceReservationIdsToFinsish(CancellationToken cancellationToken);
}