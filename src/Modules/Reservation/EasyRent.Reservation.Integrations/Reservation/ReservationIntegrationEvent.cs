using System;
using EasyRent.MessageBroker;

namespace EasyRent.Reservation.Events.Reservation;

public record ReservationIntegrationEvent : IntegrationEvent, IReservationIntegrationEvent
{
    public ReservationEventType EventType { get; init; } = ReservationEventType.Undefined;
    public Guid ReservationId { get; init; }
    public Guid RentalAdId { get; init; }
}