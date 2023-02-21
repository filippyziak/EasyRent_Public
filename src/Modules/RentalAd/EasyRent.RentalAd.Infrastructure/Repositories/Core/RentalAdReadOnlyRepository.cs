using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EasyRent.Infrastructure.Extensions;
using EasyRent.RentalAd.Core.Abstractions;
using EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAds;
using EasyRent.RentalAd.Infrastructure.DocumentStore.Documents;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.RentalAd.ReadModels.Dtos;
using EasyRent.RentalAd.ReadModels.ReadModels;
using EasyRent.Shared.Pagination;

namespace EasyRent.RentalAd.Infrastructure.Repositories.Core;

public class RentalAdReadOnlyRepository : IRentalAdReadOnlyRepository
{
    private readonly IRentalAdDocumentRepository _rentalAdDocumentRepository;
    private readonly IMapper _mapper;

    public RentalAdReadOnlyRepository(IRentalAdDocumentRepository rentalAdDocumentRepository,
        IMapper mapper)
    {
        _rentalAdDocumentRepository = rentalAdDocumentRepository;
        _mapper = mapper;
    }

    public Task<IReadOnlyList<string>> GetRentalAdIdsByPlaceOwnerIdAsync(Guid placeOwnerId, CancellationToken cancellationToken = default)
        => _rentalAdDocumentRepository.GetRentalAdIdsByPlaceOwnerIdAsync(placeOwnerId, cancellationToken);

    public async Task<RentalAdReadModel> GetRentalAdByIdAsync(Guid rentalAdId, CancellationToken cancellationToken = default)
        => _mapper.Map<RentalAdReadModel>(await _rentalAdDocumentRepository.FindAsync(r => r.RentalAdId == rentalAdId.ToString(), cancellationToken));


    public async Task<IPagedList<RentalAdReadModel>> GetRentalAdsAsync(GetRentalAdsQuery query, CancellationToken cancellationToken = default)
    {
        return _mapper
            .ToPagedList<RentalAdReadModel, RentalAdDocument>(await _rentalAdDocumentRepository
                .GetFilteredRentalAdsAsync(query, cancellationToken));
    }

    public async Task<IReadOnlyList<RentalAdReadModel>> GetRentalAdsForPlaceOwnerAsync(Guid placeOwnerId, CancellationToken cancellationToken = default)
        => _mapper
            .Map<IReadOnlyList<RentalAdReadModel>>(await _rentalAdDocumentRepository
                .GetRentalAdsForPlaceOwnerAsync(placeOwnerId,
                    cancellationToken));
}