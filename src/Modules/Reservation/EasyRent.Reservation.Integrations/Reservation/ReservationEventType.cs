namespace EasyRent.Reservation.Events.Reservation;

public enum ReservationEventType
{
    Undefined = -1,
    ReservationCreated = 0,
    ReservationCanceled = 1,
    ReservationFinished = 2,
    ReservationReviewed = 3,
    ReservationUpdated = 4
}