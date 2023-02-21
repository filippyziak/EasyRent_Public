using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.RentalAd.ReadModels.ReadModels;

namespace EasyRent.RentalAd.Core.Abstractions;

public interface IPlaceFeatureReadOnlyRepository
{
    Task<PlaceFeatureReadModel> GetPlaceFeatureByIdAsync(Guid placeFeatureId, CancellationToken token = default);
}