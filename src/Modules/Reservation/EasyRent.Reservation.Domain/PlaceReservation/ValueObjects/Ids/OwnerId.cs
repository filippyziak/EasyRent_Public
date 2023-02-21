using System;
using EasyRent.EventSourcing;

namespace EasyRent.Reservation.Domain.PlaceReservation.ValueObjects.Ids;

public record OwnerId(Guid Value) : EntityId(Value);