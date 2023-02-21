namespace EasyRent.Reservation.Events.Reservation;

public record ReservationCreatedIntegrationEvent : ReservationIntegrationEvent
{
    public ReservationCreatedIntegrationEvent() => EventType = ReservationEventType.ReservationCreated;
}