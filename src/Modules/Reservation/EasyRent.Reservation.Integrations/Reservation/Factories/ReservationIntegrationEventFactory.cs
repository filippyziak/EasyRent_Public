using System;
using EasyRent.MessageBroker;
using EasyRent.Reservation.Events.Reservation.Data;

namespace EasyRent.Reservation.Events.Reservation.Factories;

public static class ReservationIntegrationEventFactory
{
    public static string ExchangeName => "reservation";

    public static ReservationCreatedIntegrationEvent Create(Guid reservationId,
        DateTime arrivalDate,
        DateTime departureDate,
        Guid tenantId,
        Guid rentalAdId)
        => IntegrationEventFactory.WithData<ReservationCreatedIntegrationEvent>(
                new ReservationCreatedIntegrationEventData(arrivalDate,
                    departureDate,
                    tenantId))
            with
            {
                ReservationId = reservationId,
                RentalAdId = rentalAdId
            };

    public static ReservationCanceledIntegrationEvent Cancel(Guid reservationId, Guid rentalAdId)
        => IntegrationEventFactory.WithoutData<ReservationCanceledIntegrationEvent>()
            with
            {
                ReservationId = reservationId,
                RentalAdId = rentalAdId
            };
}