using System;

namespace EasyRent.Reservation.Events.Reservation;

public interface IReservationIntegrationEvent
{
    ReservationEventType EventType { get; }
    Guid ReservationId { get; }
}