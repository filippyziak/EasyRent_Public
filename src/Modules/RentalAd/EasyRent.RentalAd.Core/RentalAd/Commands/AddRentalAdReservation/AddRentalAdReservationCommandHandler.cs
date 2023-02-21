using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.AddRentalAdReservation;

public class AddRentalAdReservationCommandHandler : IRequestHandler<AddRentalAdReservationCommand, AddRentalAdReservationResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;

    public AddRentalAdReservationCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<AddRentalAdReservationResponse> Handle(AddRentalAdReservationCommand request, CancellationToken cancellationToken)
    {
        if (!await _aggregateRepository.ExistsAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId)))
        {
            throw new EntityNotFoundException(request.RentalAdId, typeof(Domain.RentalAd.RentalAd));
        }

        var rentalAd = await _aggregateRepository.LoadAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId));

        if (request.DepartureDate != null)
        {
            rentalAd.AddPlaceReservation(new PlaceReservationId(request.PlaceReservationId),
                PlaceReservationPeriodDates.ForRange(request.ArrivalDate, request.DepartureDate.Value));
        }
        else
        {
            rentalAd.AddPlaceReservation(new PlaceReservationId(request.PlaceReservationId),
                PlaceReservationPeriodDates.OneNight(request.ArrivalDate));
        }

        await _aggregateRepository.SaveAsync<Domain.RentalAd.RentalAd, RentalAdId>(rentalAd);

        _logger.Info("Reservation added to Rental Ad with ID: {RentalAdId} in event store", rentalAd.Id.Value);

        return new AddRentalAdReservationResponse();
    }
}