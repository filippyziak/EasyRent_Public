using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Management.Core.Abstractions;
using EasyRent.Management.ReadModels.ReadModels;
using EasyRent.Management.Shared.Abstractions;

namespace EasyRent.Management.Infrastructure.Facades;

public class ManagementContextFacade : IManagementContextFacade
{
    private readonly IPlaceFeatureReadOnlyRepository _placeFeatureReadOnlyRepository;

    public ManagementContextFacade(IPlaceFeatureReadOnlyRepository placeFeatureReadOnlyRepository)
    {
        _placeFeatureReadOnlyRepository = placeFeatureReadOnlyRepository;
    }

    public Task<PlaceFeatureReadModel> GetPlaceFeatureByIdAsync(Guid placeFeatureId, CancellationToken cancellationToken = default)
        => _placeFeatureReadOnlyRepository.GetPlaceFeatureById(placeFeatureId, cancellationToken);
}