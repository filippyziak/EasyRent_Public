using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.MessageBroker;
using EasyRent.Reservation.Core.Abstractions;
using EasyRent.Reservation.Domain.PlaceReservation;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects.Ids;
using EasyRent.Reservation.Events.Reservation.Factories;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Commands.CreatePlaceReservation;

public class CreatePlaceReservationCommandHandler : IRequestHandler<CreatePlaceReservationCommand, CreatePlaceReservationResponse>
{
    private readonly ILogger _logger;
    private readonly IRentalAdReadOnlyRepository _rentalAdReadOnlyRepository;
    private readonly ITenantReadOnlyRepository _tenantReadOnlyRepository;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IReservationReadOnlyRepository _reservationReadOnly;
    private readonly IMessagePublisher _messagePublisher;

    public CreatePlaceReservationCommandHandler(ILogger logger,
        IRentalAdReadOnlyRepository rentalAdReadOnlyRepository,
        ITenantReadOnlyRepository tenantReadOnlyRepository,
        IAggregateRepository aggregateRepository,
        IReservationReadOnlyRepository reservationReadOnly,
        IMessagePublisher messagePublisher)
    {
        _logger = logger;
        _rentalAdReadOnlyRepository = rentalAdReadOnlyRepository;
        _tenantReadOnlyRepository = tenantReadOnlyRepository;
        _aggregateRepository = aggregateRepository;
        _reservationReadOnly = reservationReadOnly;
        _messagePublisher = messagePublisher;
    }

    public async Task<CreatePlaceReservationResponse> Handle(CreatePlaceReservationCommand request, CancellationToken cancellationToken)
    {
        if (!await _rentalAdReadOnlyRepository.AnyRentalAdExistsAsync(request.RentalAdId, cancellationToken))
        {
            throw new EntityNotFoundException(request.RentalAdId, typeof(RentalAdId));
        }

        if (!await _tenantReadOnlyRepository.AnyTenantExistsAsync(request.TentantId, cancellationToken))
        {
            throw new EntityNotFoundException(request.TentantId, typeof(TenantId));
        }

        if ((await _reservationReadOnly.GetPlaceReservationsByRentalAdId(request.RentalAdId, cancellationToken))
            .Any(pr => (pr.ArrivalDate <= request.ArrivalDate
                       && pr.DepartureDate >= request.ArrivalDate)
                       || (pr.ArrivalDate <= request.DepartureDate
                       && pr.DepartureDate >= request.DepartureDate)
                       || (pr.ArrivalDate >= request.ArrivalDate
                           && pr.DepartureDate >= request.ArrivalDate
                           && pr.ArrivalDate <= request.DepartureDate
                           && pr.DepartureDate <= request.DepartureDate)))
        {
            throw new EntityAlreadyExistsException(request.RentalAdId, typeof(RentalAdId));
        }

        var reservation = new PlaceReservation(new PlaceReservationId(Guid.NewGuid()),
            PlaceReservationPeriodDates.ForRange(request.ArrivalDate, request.DepartureDate),
            new TenantId(request.TentantId),
            new OwnerId(request.OwnerId),
            new RentalAdId(request.RentalAdId));

        await _aggregateRepository.SaveAsync<PlaceReservation, PlaceReservationId>(reservation);

        await _messagePublisher.PublishMessageAsync(ReservationIntegrationEventFactory.ExchangeName,
            ReservationIntegrationEventFactory.Create(
                reservation.Id,
                request.ArrivalDate,
                request.DepartureDate,
                request.TentantId,
                request.RentalAdId), cancellationToken);

        _logger.Info("Place reservation with Id: {ReservationId} stored", reservation.Id);

        return new CreatePlaceReservationResponse();
    }
}