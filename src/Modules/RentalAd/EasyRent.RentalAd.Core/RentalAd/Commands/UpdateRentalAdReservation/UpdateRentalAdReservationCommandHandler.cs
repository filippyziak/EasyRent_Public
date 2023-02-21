using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdReservation;

public class UpdateRentalAdReservationCommandHandler : IRequestHandler<UpdateRentalAdReservationCommand, UpdateRentalAdReservationResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;

    public UpdateRentalAdReservationCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<UpdateRentalAdReservationResponse> Handle(UpdateRentalAdReservationCommand request, CancellationToken cancellationToken)
    {
        if (!await _aggregateRepository.ExistsAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId)))
        {
            throw new EntityNotFoundException(request.RentalAdId, typeof(Domain.RentalAd.RentalAd));
        }

        var rentalAd = await _aggregateRepository.LoadAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId));

        var placeReservation = rentalAd.PlaceReservations.FirstOrDefault(p
                                   => p.PlaceReservationId == request.ReservationId)
                               ?? throw new EntityNotFoundException(request.ReservationId, typeof(PlaceReservation));

        if (placeReservation.ReviewScore.Value != request.ReviewScore)
        {
            placeReservation.UpdateReviewScore(PlaceReservationReviewScore.FromInt(request.ReviewScore));
        }

        if (placeReservation.ReviewDescription.Value != request.ReviewDescription)
        {
            placeReservation.UpdateReview(PlaceReservationReviewDescription.FromString(request.ReviewDescription));
        }

        await _aggregateRepository.SaveAsync<Domain.RentalAd.RentalAd, RentalAdId>(rentalAd);

        _logger.Info("Rental Ad Review with ID: {RentalAdReviewId} has been updated in event store", request.ReservationId);

        return new UpdateRentalAdReservationResponse();
    }
}