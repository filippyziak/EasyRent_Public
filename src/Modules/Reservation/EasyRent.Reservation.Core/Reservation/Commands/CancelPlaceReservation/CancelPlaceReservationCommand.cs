using System;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Commands.CancelPlaceReservation;

public record CancelPlaceReservationCommand : IRequest<CancelPlaceReservationResponse>
{
    public Guid PlaceReservationId { get; init; }
    public Guid CurrentAccountId { get; init; }
}