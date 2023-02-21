using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Management.ReadModels.ReadModels;

namespace EasyRent.Management.Core.Abstractions;

public interface IPlaceFeatureReadOnlyRepository
{
    Task<PlaceFeatureReadModel> GetPlaceFeatureById(Guid placeFeatureId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlaceFeatureReadModel>> GetPlaceFeatures(CancellationToken cancellationToken = default);
}