using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Commands.FinishPlaceReservations;

public record FinishPlaceReservationsCommand : IRequest<FinishPlaceReservationsResponse>;