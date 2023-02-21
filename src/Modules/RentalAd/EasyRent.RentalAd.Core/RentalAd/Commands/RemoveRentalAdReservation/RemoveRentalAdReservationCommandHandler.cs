using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.RemoveRentalAdReservation;

public class RemoveRentalAdReservationCommandHandler : IRequestHandler<RemoveRentalAdReservationCommand,RemoveRentalAdReservationResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;

    public RemoveRentalAdReservationCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
    }
    
    public async Task<RemoveRentalAdReservationResponse> Handle(RemoveRentalAdReservationCommand request, CancellationToken cancellationToken)
    {
        if (!await _aggregateRepository.ExistsAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId)))
        {
            throw new EntityNotFoundException(request.RentalAdId, typeof(Domain.RentalAd.RentalAd));
        }

        var rentalAd = await _aggregateRepository.LoadAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId));

        rentalAd.RemovePlaceReservation(new PlaceReservationId(request.RentalAdReservationId));

        await _aggregateRepository.SaveAsync<Domain.RentalAd.RentalAd, RentalAdId>(rentalAd);

        _logger.Info("Place reservation removed from Rental Ad with ID: {RentalAdId} in event store", rentalAd.Id.Value);

        return new RemoveRentalAdReservationResponse();
    }
}