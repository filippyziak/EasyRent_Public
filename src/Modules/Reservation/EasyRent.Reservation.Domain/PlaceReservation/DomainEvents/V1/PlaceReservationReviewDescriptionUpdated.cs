using System;
using EasyRent.EventSourcing;

namespace EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;

public record PlaceReservationReviewDescriptionUpdated(
    Guid PlaceReservationId,
    string ReviewDescription
) : IDomainEvent;