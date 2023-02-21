using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Management.Domain.PlaceFeatures;
using EasyRent.Management.Domain.PlaceFeatures.ValueObjects;
using MediatR;

namespace EasyRent.Management.Core.Handlers.PlaceFeatures.Command.CreatePlaceFeature;

public class CreatePlaceFeatureCommandHandler : IRequestHandler<CreatePlaceFeatureCommand, CreatePlaceFeatureResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;

    public CreatePlaceFeatureCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<CreatePlaceFeatureResponse> Handle(CreatePlaceFeatureCommand request, CancellationToken cancellationToken)
    {
        var placeFeature = new PlaceFeature(new PlaceFeatureId(Guid.NewGuid()),
            PlaceFeatureDescription.FromString(request.FeatureDescription),
            PlaceFeaturePictureUrl.FromString(request.FeaturePictureUrl));

        await _aggregateRepository.SaveAsync<PlaceFeature, PlaceFeatureId>(placeFeature);

        _logger.Info("Place Feature with ID: {FeatureId} stored in the event stream",
            placeFeature.Id.Value);

        return new CreatePlaceFeatureResponse();
    }
}