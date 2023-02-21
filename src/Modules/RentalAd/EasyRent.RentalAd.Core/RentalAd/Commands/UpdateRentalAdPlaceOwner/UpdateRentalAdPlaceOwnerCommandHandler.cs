using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Core.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdPlaceOwner;

public class UpdateRentalAdPlaceOwnerCommandHandler : IRequestHandler<UpdateRentalAdPlaceOwnerCommand, UpdateRentalAdPlaceOwnerResponse>
{
    private readonly ILogger _logger;
    private readonly IRentalAdReadOnlyRepository _rentalAdReadOnlyRepository;
    private readonly IAggregateRepository _aggregateRepository;

    public UpdateRentalAdPlaceOwnerCommandHandler(ILogger logger,
        IRentalAdReadOnlyRepository rentalAdReadOnlyRepository,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _rentalAdReadOnlyRepository = rentalAdReadOnlyRepository;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<UpdateRentalAdPlaceOwnerResponse> Handle(UpdateRentalAdPlaceOwnerCommand request, CancellationToken cancellationToken)
    {
        var rentalAdIds = await _rentalAdReadOnlyRepository.GetRentalAdIdsByPlaceOwnerIdAsync(request.PlaceOwnerId, cancellationToken);

        await Task.WhenAll(rentalAdIds.Select(r => UpdatePlaceOwner(request, r)));

        return new UpdateRentalAdPlaceOwnerResponse();
    }

    private async Task UpdatePlaceOwner(UpdateRentalAdPlaceOwnerCommand request, string rentalAdId)
    {
        var rentalAd = await _aggregateRepository.LoadAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(new Guid(rentalAdId)));

        if (rentalAd.PlaceOwner.EmailAddress.Value != request.NewEmailAddress)
        {
            rentalAd.PlaceOwner.UpdateEmailAddress(PlaceOwnerEmailAddress.FromString(request.NewEmailAddress));
        }

        await _aggregateRepository.SaveAsync<Domain.RentalAd.RentalAd, RentalAdId>(rentalAd);

        _logger.Info("Rental Ad for place owner with ID: {PlaceOwnerId} has been updated in event store", request.PlaceOwnerId);
    }
}