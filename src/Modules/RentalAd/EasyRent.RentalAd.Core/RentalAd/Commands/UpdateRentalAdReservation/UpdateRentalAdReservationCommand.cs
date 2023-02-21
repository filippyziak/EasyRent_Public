using System;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdReservation;

public record UpdateRentalAdReservationCommand : IRequest<UpdateRentalAdReservationResponse>
{
    public Guid RentalAdId { get; init; }
    public Guid ReservationId { get; init; }
    public string ReviewDescription { get; init; }
    public int ReviewScore { get; init; }
}