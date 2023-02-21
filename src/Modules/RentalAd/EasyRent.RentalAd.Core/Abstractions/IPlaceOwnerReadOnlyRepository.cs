using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.RentalAd.ReadModels.ReadModels;

namespace EasyRent.RentalAd.Core.Abstractions;

public interface IPlaceOwnerReadOnlyRepository
{
    Task<PlaceOwnerReadModel> GetPlaceOwnerByIdAsync(Guid placeOwnerId, CancellationToken cancellationToken = default);
}