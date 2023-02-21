using System;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Commands.CreatePlaceReservation;

public record CreatePlaceReservationCommand : IRequest<CreatePlaceReservationResponse>
{
    public Guid RentalAdId { get; init; }
    public Guid TentantId { get; init; }
    public Guid OwnerId { get; init; }
    public DateTime ArrivalDate { get; init; }
    public DateTime DepartureDate { get; init; }
}