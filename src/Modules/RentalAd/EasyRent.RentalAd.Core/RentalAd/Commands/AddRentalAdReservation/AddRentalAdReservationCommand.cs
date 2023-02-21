using System;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.AddRentalAdReservation;

public record AddRentalAdReservationCommand : IRequest<AddRentalAdReservationResponse>
{
    public Guid RentalAdId { get; init; }
    public  Guid PlaceReservationId { get; init; }
    public  DateTime ArrivalDate { get; init; }
    public  DateTime? DepartureDate { get; init; }
}