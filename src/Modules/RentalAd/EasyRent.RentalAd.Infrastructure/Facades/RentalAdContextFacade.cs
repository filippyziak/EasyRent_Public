using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.RentalAd.Core.Abstractions;
using EasyRent.RentalAd.Shared.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Facades;

public class RentalAdContextFacade : IRentalAdContextFacade
{
    private readonly IRentalAdReadOnlyRepository _rentalAdReadOnlyRepository;

    public RentalAdContextFacade(IRentalAdReadOnlyRepository rentalAdReadOnlyRepository)
    {
        _rentalAdReadOnlyRepository = rentalAdReadOnlyRepository;
    }

    public async Task<bool> AnyRentalAdAsync(Guid rentalAdId, CancellationToken cancellationToken = default)
        => await _rentalAdReadOnlyRepository.GetRentalAdByIdAsync(rentalAdId) is not null;
}