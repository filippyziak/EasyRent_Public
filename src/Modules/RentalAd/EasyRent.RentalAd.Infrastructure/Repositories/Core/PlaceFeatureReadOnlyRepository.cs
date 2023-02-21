using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EasyRent.Management.Shared.Abstractions;
using EasyRent.RentalAd.Core.Abstractions;
using EasyRent.RentalAd.ReadModels.ReadModels;

namespace EasyRent.RentalAd.Infrastructure.Repositories.Core;

public class PlaceFeatureReadOnlyRepository : IPlaceFeatureReadOnlyRepository
{
    private readonly IManagementContextFacade _managementContextFacade;
    private readonly IMapper _mapper;

    public PlaceFeatureReadOnlyRepository(IManagementContextFacade managementContextFacade,
        IMapper mapper)
    {
        _managementContextFacade = managementContextFacade;
        _mapper = mapper;
    }

    public async Task<PlaceFeatureReadModel> GetPlaceFeatureByIdAsync(Guid placeFeatureId, CancellationToken token = default)
        =>_mapper.Map<PlaceFeatureReadModel>(await _managementContextFacade.GetPlaceFeatureByIdAsync(placeFeatureId, token));
}