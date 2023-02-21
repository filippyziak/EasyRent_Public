using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Core.Abstractions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAdsForPlaceOwner;

public class GetRentalAdsForPlaceOwnerQueryHandler : IRequestHandler<GetRentalAdsForPlaceOwnerQuery, GetRentalAdsForPlaceOwnerResponse>
{
    private readonly ILogger _logger;
    private readonly IRentalAdReadOnlyRepository _rentalAdReadOnlyRepository;

    public GetRentalAdsForPlaceOwnerQueryHandler(ILogger logger,
        IRentalAdReadOnlyRepository rentalAdReadOnlyRepository)
    {
        _logger = logger;
        _rentalAdReadOnlyRepository = rentalAdReadOnlyRepository;
    }

    public async Task<GetRentalAdsForPlaceOwnerResponse> Handle(GetRentalAdsForPlaceOwnerQuery request, CancellationToken cancellationToken)
    {
        var rentalAds = await _rentalAdReadOnlyRepository.GetRentalAdsForPlaceOwnerAsync(request.PlaceOwnerId, cancellationToken);

        _logger.Info("{RentalAdCount} rental ads fetched", rentalAds.Count);

        return new GetRentalAdsForPlaceOwnerResponse
        {
            RentalAds = rentalAds,
        };
    }
}