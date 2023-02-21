using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EasyRent.Management.Core.Abstractions;
using EasyRent.Management.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.Management.ReadModels.ReadModels;

namespace EasyRent.Management.Infrastructure.Repositories.Core;

public class PlaceFeatureReadOnlyRepository : IPlaceFeatureReadOnlyRepository
{
    private readonly IPlaceFeatureDocumentRepository _featureDocumentRepository;
    private readonly IMapper _mapper;

    public PlaceFeatureReadOnlyRepository(IPlaceFeatureDocumentRepository featureDocumentRepository,
        IMapper mapper)
    {
        _featureDocumentRepository = featureDocumentRepository;
        _mapper = mapper;
    }

    public async Task<PlaceFeatureReadModel> GetPlaceFeatureById(Guid placeFeatureId, CancellationToken cancellationToken = default)
        => _mapper.Map<PlaceFeatureReadModel>(await _featureDocumentRepository.FindAsync(p => p.PlaceFeatureId == placeFeatureId));

    public async Task<IReadOnlyList<PlaceFeatureReadModel>> GetPlaceFeatures(CancellationToken cancellationToken = default)
        => _mapper.Map<IReadOnlyList<PlaceFeatureReadModel>>(await _featureDocumentRepository.GetPlaceFeatures(cancellationToken));
}