using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdAddress;

public class UpdateRentalAdAddressCommandHandler : IRequestHandler<UpdateRentalAdAddressCommand, UpdateRentalAdAddressResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;

    public UpdateRentalAdAddressCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<UpdateRentalAdAddressResponse> Handle(UpdateRentalAdAddressCommand request, CancellationToken cancellationToken)
    {
        if (!await _aggregateRepository.ExistsAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId)))
        {
            throw new EntityNotFoundException(request.RentalAdId, typeof(Domain.RentalAd.RentalAd));
        }

        var rentalAd = await _aggregateRepository.LoadAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId));

        if (rentalAd.PlaceOwner.PlaceOwnerId != request.CurrentOwnerId)
        {
            throw new PermissionException(typeof(Domain.RentalAd.RentalAd));
        }
        
        if (request.NewCountry != rentalAd.PlaceAddress.Country)
        {
            rentalAd.PlaceAddress.UpdateCountry(PlaceAddressCountry.FromString(request.NewCountry),
                PlaceAddressCity.FromString(request.NewCity),
                PlaceAddressStreet.FromString(request.NewStreet));

            return await SaveRentalAdWithUpdatedAddress(rentalAd);
        }

        if (request.NewCity != rentalAd.PlaceAddress.City)
        {
            rentalAd.PlaceAddress.UpdateCity(PlaceAddressCity.FromString(request.NewCity),
                PlaceAddressStreet.FromString(request.NewStreet));

            return await SaveRentalAdWithUpdatedAddress(rentalAd);
        }

        if (request.NewStreet != rentalAd.PlaceAddress.Street)
        {
            rentalAd.PlaceAddress.UpdateStreet(PlaceAddressStreet.FromString(request.NewStreet));

            return await SaveRentalAdWithUpdatedAddress(rentalAd);
        }

        _logger.Info("Provided address for Rental Ad with ID:{RentalAdId} is the same", rentalAd.Id.Value);

        return new UpdateRentalAdAddressResponse();
    }

    private async Task<UpdateRentalAdAddressResponse> SaveRentalAdWithUpdatedAddress(Domain.RentalAd.RentalAd rentalAd)
    {
        await _aggregateRepository.SaveAsync<Domain.RentalAd.RentalAd, RentalAdId>(rentalAd);

        _logger.Info("Rental Ad with ID: {RentalAdId} place address has been updated in event store", rentalAd.Id.Value);

        return new UpdateRentalAdAddressResponse();
    }
}