using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyRent.RentalAd.Shared.Abstractions;

public interface IRentalAdContextFacade
{
    Task<bool> AnyRentalAdAsync(Guid rentalAdId, CancellationToken cancellationToken = default);
}