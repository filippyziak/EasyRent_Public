using System;
using EasyRent.EventSourcing;

namespace EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;

public record PlaceReservationReviewed(
    Guid PlaceReservationId,
    string ReviewDescription,
    int ReviewScore
) : IDomainEvent;