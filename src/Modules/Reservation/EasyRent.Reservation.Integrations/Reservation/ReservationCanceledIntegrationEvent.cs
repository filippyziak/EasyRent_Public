namespace EasyRent.Reservation.Events.Reservation;

public record ReservationCanceledIntegrationEvent : ReservationIntegrationEvent
{
    public ReservationCanceledIntegrationEvent() => EventType = ReservationEventType.ReservationCanceled;
}