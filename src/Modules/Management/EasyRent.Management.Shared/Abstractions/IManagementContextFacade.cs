using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Management.ReadModels.ReadModels;

namespace EasyRent.Management.Shared.Abstractions;

public interface IManagementContextFacade
{
    public Task<PlaceFeatureReadModel> GetPlaceFeatureByIdAsync(Guid placeFeatureId, CancellationToken cancellationToken = default);
}