using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyRent.Reservation.Core.Abstractions;

public interface ITenantReadOnlyRepository
{
    Task<bool> AnyTenantExistsAsync(Guid tenantId, CancellationToken cancellationToken = default);
}