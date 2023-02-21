using System;
using EasyRent.EventSourcing;

namespace EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;

public record PlaceReservationCreated(
    Guid PlaceReservationId,
    DateTime ArrivalDate,
    DateTime DepartureDate,
    Guid TenantId,
    Guid OwnerId,
    Guid RentalAdId
) : IDomainEvent;