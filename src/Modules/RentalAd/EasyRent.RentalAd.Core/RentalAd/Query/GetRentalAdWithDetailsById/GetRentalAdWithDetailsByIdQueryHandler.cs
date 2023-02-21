using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Core.Abstractions;
using EasyRent.RentalAd.ReadModels.ReadModels;
using EasyRent.Shared.Exceptions;
using EasyRent.Shared.Extensions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAdWithDetailsById;

public class GetRentalAdWithDetailsByIdQueryHandler : IRequestHandler<GetRentalAdWithDetailsByIdQuery, GetRentalAdWithDetailsByIdResponse>
{
    private readonly ILogger _logger;
    private readonly IRentalAdReadOnlyRepository _rentalAdReadOnlyRepository;

    public GetRentalAdWithDetailsByIdQueryHandler(ILogger logger,
        IRentalAdReadOnlyRepository rentalAdReadOnlyRepository)
    {
        _logger = logger;
        _rentalAdReadOnlyRepository = rentalAdReadOnlyRepository;
    }

    public async Task<GetRentalAdWithDetailsByIdResponse> Handle(GetRentalAdWithDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var rentalAd = await _rentalAdReadOnlyRepository.GetRentalAdByIdAsync(request.RentalAdId, cancellationToken)
                       ?? throw new EntityNotFoundException(request.RentalAdId, typeof(RentalAdReadModel));

        _logger.Info("Rental Ad with ID: #{RentalAdId} fetched");

        if (rentalAd.PlaceReservations.Any())
        {
            var reservedDates = new List<DateTime>();
            foreach (var reservation in rentalAd.PlaceReservations)
            {
                reservedDates.AddRange(reservation.ArrivalDate.Range(reservation.DepartureDate));
            }

            rentalAd = rentalAd with
            {
                ReservedDates = reservedDates
            };
        }

        return new GetRentalAdWithDetailsByIdResponse
        {
            RentalAd = rentalAd
        };
    }
}