using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EasyRent.Management.Core.Handlers.PlaceFeatures.Query.GetPlaceFeatures;

public class GetPlaceFeaturesQueryHandler : IRequestHandler<GetPlaceFeaturesQuery, GetPlaceFeaturesResponse>
{
    public async Task<GetPlaceFeaturesResponse> Handle(GetPlaceFeaturesQuery request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}