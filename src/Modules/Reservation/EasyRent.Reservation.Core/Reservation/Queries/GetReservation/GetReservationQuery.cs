using System;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Queries.GetReservation;

public record GetReservationQuery : IRequest<GetReservationResponse>
{
    public Guid ReservationId { get; init; }
}