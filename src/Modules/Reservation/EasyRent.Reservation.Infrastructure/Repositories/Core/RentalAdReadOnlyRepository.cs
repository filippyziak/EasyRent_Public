using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.RentalAd.Shared.Abstractions;
using EasyRent.Reservation.Core.Abstractions;

namespace EasyRent.Reservation.Infrastructure.Repositories.Core;

public class RentalAdReadOnlyRepository : IRentalAdReadOnlyRepository
{
    private readonly IRentalAdContextFacade _rentalAdContextFacade;

    public RentalAdReadOnlyRepository(IRentalAdContextFacade rentalAdContextFacade)
    {
        _rentalAdContextFacade = rentalAdContextFacade;
    }
    
    public Task<bool> AnyRentalAdExistsAsync(Guid rentalAdId, CancellationToken cancellationToken = default)
        => _rentalAdContextFacade.AnyRentalAdAsync(rentalAdId, cancellationToken);
}