using System;
using EasyRent.EventSourcing;

namespace EasyRent.Reservation.Domain.PlaceReservation.ValueObjects.Ids;

public record RentalAdId(Guid Value) : AggregateId(Value);