using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EasyRent.Identity.Shared.Abstractions;
using EasyRent.RentalAd.Core.Abstractions;
using EasyRent.RentalAd.ReadModels.ReadModels;

namespace EasyRent.RentalAd.Infrastructure.Repositories.Core;

public class PlaceOwnerReadOnlyRepository : IPlaceOwnerReadOnlyRepository
{
    private readonly IIdentityContextFacade _identityContextFacade;
    private readonly IMapper _mapper;

    public PlaceOwnerReadOnlyRepository(IIdentityContextFacade identityContextFacade,
        IMapper mapper)
    {
        _identityContextFacade = identityContextFacade;
        _mapper = mapper;
    }

    public async Task<PlaceOwnerReadModel> GetPlaceOwnerByIdAsync(Guid placeOwnerId, CancellationToken cancellationToken = default)
        => _mapper.Map<PlaceOwnerReadModel>(await _identityContextFacade.GetGetAccountByIdAsync(placeOwnerId, cancellationToken));
}