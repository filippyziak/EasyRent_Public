using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Identity.Shared.Abstractions;
using EasyRent.Reservation.Core.Abstractions;

namespace EasyRent.Reservation.Infrastructure.Repositories.Core;

public class TenantReadOnlyRepository : ITenantReadOnlyRepository
{
    private readonly IIdentityContextFacade _identityContextFacade;

    public TenantReadOnlyRepository(IIdentityContextFacade identityContextFacade)
    {
        _identityContextFacade = identityContextFacade;
    }

    public async Task<bool> AnyTenantExistsAsync(Guid tenantId, CancellationToken cancellationToken = default)
        => await _identityContextFacade.GetGetAccountByIdAsync(tenantId) is not null;
}