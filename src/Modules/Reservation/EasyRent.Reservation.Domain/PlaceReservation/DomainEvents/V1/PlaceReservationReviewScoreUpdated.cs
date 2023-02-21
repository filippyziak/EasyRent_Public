using System;
using EasyRent.EventSourcing;

namespace EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;

public record PlaceReservationReviewScoreUpdated(
    Guid PlaceReservationId,
    int ReviewScore
) : IDomainEvent;