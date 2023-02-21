using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyRent.Reservation.Core.Abstractions;

public interface IRentalAdReadOnlyRepository
{
    Task<bool> AnyRentalAdExistsAsync(Guid rentalAdId, CancellationToken cancellationToken = default);
}