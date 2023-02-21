using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EasyRent.Infrastructure.Extensions;
using EasyRent.Reservation.Core.Abstractions;
using EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForRentalAd;
using EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForTenant;
using EasyRent.Reservation.Infrastructure.DocumentStore.Documents;
using EasyRent.Reservation.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.Reservation.ReadModels.ReadModels;
using EasyRent.Shared.Pagination;

namespace EasyRent.Reservation.Infrastructure.Repositories.Core;

public class ReservationReadOnlyRepository : IReservationReadOnlyRepository
{
    private readonly IPlaceReservationDocumentRepository _placeReservationDocumentRepository;
    private readonly IMapper _mapper;

    public ReservationReadOnlyRepository(IPlaceReservationDocumentRepository placeReservationDocumentRepository,
        IMapper mapper)
    {
        _placeReservationDocumentRepository = placeReservationDocumentRepository;
        _mapper = mapper;
    }


    public async Task<IPagedList<PlaceReservationReadModel>> GetPlaceReservationsByTenantId(GetReservationsForTenantQuery query, CancellationToken cancellationToken = default)
        => _mapper.ToPagedList<PlaceReservationReadModel, PlaceReservationDocument>(await _placeReservationDocumentRepository.GetPlaceReservationsByTenantId(query,
cancellationToken));

    public async Task<IPagedList<PlaceReservationReadModel>> GetPlaceReservationsByRentalAdId(GetReservationsForRentalAdQuery query, CancellationToken cancellationToken = default)
        => _mapper.ToPagedList<PlaceReservationReadModel, PlaceReservationDocument>(await _placeReservationDocumentRepository.GetPlaceReservationsByRentalAdId(query,
    cancellationToken));

    public async Task<IReadOnlyList<PlaceReservationReadModel>> GetPlaceReservationsByRentalAdId(Guid rentalAdId, CancellationToken cancellationToken = default)
        => _mapper.Map<IReadOnlyList<PlaceReservationReadModel>>(await _placeReservationDocumentRepository.GetPlaceReservationsByRentalAdId(rentalAdId,
            cancellationToken));

    public async Task<PlaceReservationReadModel> GetPlaceReservationById(Guid placeReservationId, CancellationToken cancellationToken = default)
        => _mapper.Map<PlaceReservationReadModel>(await _placeReservationDocumentRepository.FindAsync(pr
            => pr.PlaceReservationId == placeReservationId));

    public Task<IReadOnlyList<Guid>> GetPlaceReservationIdsToFinsish(CancellationToken cancellationToken)
        => _placeReservationDocumentRepository.GetPlaceReservationIdsToFinsish(cancellationToken);
}