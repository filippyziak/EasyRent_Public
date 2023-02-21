using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.MessageBroker;
using EasyRent.Reservation.Domain.PlaceReservation;
using EasyRent.Reservation.Domain.PlaceReservation.States;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Commands.UpdateReview;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, UpdateReviewResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IMessagePublisher _messagePublisher;

    public UpdateReviewCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository,
        IMessagePublisher messagePublisher)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
        _messagePublisher = messagePublisher;
    }

    public async Task<UpdateReviewResponse> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var placeReservation = await _aggregateRepository.LoadAsync<PlaceReservation, PlaceReservationId>(new PlaceReservationId(request.PlaceReservationId))
                               ?? throw new EntityNotFoundException(request.PlaceReservationId, typeof(PlaceReservation));

        if (placeReservation.State == PlaceReservationState.Reviewed)
        {
            if (request.NewReviewScore is not null
                && request.NewReviewScore != placeReservation.ReviewScore)
            {
                placeReservation.UpdateReviewScore(PlaceReservationReviewScore.FromInt(request.NewReviewScore.Value));
            }

            if (request.NewReviewDescription is not null
                && request.NewReviewDescription != placeReservation.ReviewDescription)
            {
                placeReservation.UpdateReviewDescription(PlaceReservationReviewDescription.FromString(request.NewReviewDescription));
            }

            _logger.Info("Place reservation with ID: #{PlaceReservationId} Review has been updated", request.PlaceReservationId);
        }

        return new UpdateReviewResponse();
    }
}