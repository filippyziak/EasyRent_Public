using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdPricePerDay;

public record UpdateRentalAdPricePerDayCommandHandler : IRequestHandler<UpdateRentalAdPricePerDayCommand, UpdateRentalAdPricePerDayResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;

    public UpdateRentalAdPricePerDayCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<UpdateRentalAdPricePerDayResponse> Handle(UpdateRentalAdPricePerDayCommand request, CancellationToken cancellationToken)
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
        rentalAd.UpdatePricePerDay(RentalAdPricePerDay.FromDecimal(request.NewPricePerDay));

        await _aggregateRepository.SaveAsync<Domain.RentalAd.RentalAd, RentalAdId>(rentalAd);

        _logger.Info("Rental Ad with ID: {RentalAdId} price per day has been updated in event store", rentalAd.Id.Value);

        return new UpdateRentalAdPricePerDayResponse();
    }
}