using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Reservation.Domain.PlaceReservation;
using EasyRent.Reservation.Domain.PlaceReservation.States;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Commands.ReviewPlaceReservation;

public class ReviewPlaceReservationCommandHandler : IRequestHandler<ReviewPlaceReservationCommand, ReviewPlaceReservationResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;

    public ReviewPlaceReservationCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<ReviewPlaceReservationResponse> Handle(ReviewPlaceReservationCommand request, CancellationToken cancellationToken)
    {
        var placeReservation = await _aggregateRepository.LoadAsync<PlaceReservation, PlaceReservationId>(new PlaceReservationId(request.PlaceReservationId))
                               ?? throw new EntityNotFoundException(request.PlaceReservationId, typeof(PlaceReservation));

        if (placeReservation.TenantId != request.TentantId)
        {
            throw new PermissionException(typeof(PlaceReservation));
        }

        if (placeReservation.State != PlaceReservationState.Finished)
        {
            throw new InvalidEntityStateException(placeReservation, "Reservation state is invalid");
        }

        placeReservation.ReviewReservation(PlaceReservationReviewDescription.FromString(request.ReviewDescription),
            PlaceReservationReviewScore.FromInt(request.ReviewScore));

        await _aggregateRepository.SaveAsync<PlaceReservation, PlaceReservationId>(placeReservation);

        _logger.Info("Place reservation with ID: #{PlaceReservationId} has been reviewed", request.PlaceReservationId);

        return new ReviewPlaceReservationResponse();
    }
}