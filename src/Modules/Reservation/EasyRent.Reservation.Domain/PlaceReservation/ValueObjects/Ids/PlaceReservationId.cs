using System;
using EasyRent.EventSourcing;

namespace EasyRent.Reservation.Domain.PlaceReservation.ValueObjects.Ids;

public record PlaceReservationId(Guid Value) : AggregateId(Value);