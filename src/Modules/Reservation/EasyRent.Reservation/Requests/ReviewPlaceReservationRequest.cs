using System;

namespace EasyRent.Reservation.Requests;

public record ReviewPlaceReservationRequest(Guid PlaceReservationId,
    string ReviewDescription,
    int ReviewScore);