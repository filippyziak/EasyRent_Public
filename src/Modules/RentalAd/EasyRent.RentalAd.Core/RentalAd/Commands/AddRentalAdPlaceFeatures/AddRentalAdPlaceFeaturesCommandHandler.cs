using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Core.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.AddRentalAdPlaceFeatures;

public class AddRentalAdPlaceFeaturesCommandHandler : IRequestHandler<AddRentalAdPlaceFeaturesCommand, AddRentalAdPlaceFeaturesResponse>
{
    private readonly ILogger _logger;
    private readonly IPlaceFeatureReadOnlyRepository _placeFeatureReadOnlyRepository;
    private readonly IAggregateRepository _aggregateRepository;

    public AddRentalAdPlaceFeaturesCommandHandler(ILogger logger,
        IPlaceFeatureReadOnlyRepository placeFeatureReadOnlyRepository,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _placeFeatureReadOnlyRepository = placeFeatureReadOnlyRepository;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<AddRentalAdPlaceFeaturesResponse> Handle(AddRentalAdPlaceFeaturesCommand request, CancellationToken cancellationToken)
    {
        if (!await _aggregateRepository.ExistsAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId)))
        {
            throw new EntityNotFoundException(request.RentalAdId, typeof(Domain.RentalAd.RentalAd));
        }

        var rentalAd = await _aggregateRepository.LoadAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId));

        foreach (var addedFeatureId in request.AddedFeatureIds)
        {
            var placeFeature = await _placeFeatureReadOnlyRepository.GetPlaceFeatureByIdAsync(addedFeatureId, cancellationToken);
            rentalAd.AddPlaceFeature(new PlaceFeatureId(placeFeature.PlaceFeatureId),
                PlaceFeatureDescription.FromString(placeFeature.PlaceFeatureDescription),
                PlaceFeaturePictureUrl.FromString(placeFeature.PlaceFeatureUrl));
        }

        await _aggregateRepository.SaveAsync<Domain.RentalAd.RentalAd, RentalAdId>(rentalAd);

        _logger.Info("Place features added ti Rental Ad with ID: {RentalAdId} in event store", rentalAd.Id.Value);

        return new AddRentalAdPlaceFeaturesResponse();
    }
}