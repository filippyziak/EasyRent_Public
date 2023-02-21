using System;

namespace EasyRent.Reservation.Events.Reservation.Data;

public record ReservationCreatedIntegrationEventData(
    DateTime ArrivalDate,
    DateTime DepartureDate,
    Guid TenantId
);