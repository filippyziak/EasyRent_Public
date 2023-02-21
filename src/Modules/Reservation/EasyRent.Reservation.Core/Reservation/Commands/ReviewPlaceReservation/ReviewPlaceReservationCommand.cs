using System;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Commands.ReviewPlaceReservation;

public record ReviewPlaceReservationCommand : IRequest<ReviewPlaceReservationResponse>
{
    public Guid PlaceReservationId { get; init; }
    public Guid TentantId { get; init; }
    public string ReviewDescription { get; init; }
    public int ReviewScore { get; init; }
}