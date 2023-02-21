using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.RemoveRentalAdPlaceFeatures;

public class RemoveRentalAdPlaceFeaturesCommandHandler : IRequestHandler<RemoveRentalAdPlaceFeaturesCommand, RemoveRentalAdPlaceFeaturesResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;

    public RemoveRentalAdPlaceFeaturesCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<RemoveRentalAdPlaceFeaturesResponse> Handle(RemoveRentalAdPlaceFeaturesCommand request, CancellationToken cancellationToken)
    {
        if (!await _aggregateRepository.ExistsAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId)))
        {
            throw new EntityNotFoundException(request.RentalAdId, typeof(Domain.RentalAd.RentalAd));
        }

        var rentalAd = await _aggregateRepository.LoadAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId));

        foreach (var placeFeatureId in request.RemovedFeatureIds)
        {
            rentalAd.RemovePlaceFeature(new PlaceFeatureId(placeFeatureId));
        }

        await _aggregateRepository.SaveAsync<Domain.RentalAd.RentalAd, RentalAdId>(rentalAd);

        _logger.Info("Place feature removed from Rental Ad with ID: {RentalAdId} in event store", rentalAd.Id.Value);

        return new RemoveRentalAdPlaceFeaturesResponse();
    }
}