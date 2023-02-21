using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Core.Abstractions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAds;

public class GetRentalAdsQueryHandler : IRequestHandler<GetRentalAdsQuery, GetRentalAdsResponse>
{
    private readonly ILogger _logger;
    private readonly IRentalAdReadOnlyRepository _rentalAdReadOnlyRepository;

    public GetRentalAdsQueryHandler(ILogger logger,
        IRentalAdReadOnlyRepository rentalAdReadOnlyRepository)
    {
        _logger = logger;
        _rentalAdReadOnlyRepository = rentalAdReadOnlyRepository;
    }

    public async Task<GetRentalAdsResponse> Handle(GetRentalAdsQuery request, CancellationToken cancellationToken)
    {
        var rentalAds = await _rentalAdReadOnlyRepository.GetRentalAdsAsync(request, cancellationToken);

        _logger.Info("{RentalAdCount} rental ads fetched", rentalAds.TotalCount);

        return new GetRentalAdsResponse
        {
            RentalAds = rentalAds,
            TotalPages = rentalAds.TotalPages
        };
    }
}