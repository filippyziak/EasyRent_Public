using System;

namespace EasyRent.Reservation.Requests;

public record CreatePlaceReservationRequest(
    Guid RentalAdId,
    Guid OwnerId,
    DateTime ArrivalDate,
    DateTime DepartureDate
);