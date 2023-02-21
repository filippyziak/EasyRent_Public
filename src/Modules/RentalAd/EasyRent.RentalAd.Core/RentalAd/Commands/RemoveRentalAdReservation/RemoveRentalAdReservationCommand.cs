using System;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.RemoveRentalAdReservation;

public record RemoveRentalAdReservationCommand : IRequest<RemoveRentalAdReservationResponse>
{
    public Guid RentalAdId { get; init; }
    public Guid RentalAdReservationId { get; init; }
}