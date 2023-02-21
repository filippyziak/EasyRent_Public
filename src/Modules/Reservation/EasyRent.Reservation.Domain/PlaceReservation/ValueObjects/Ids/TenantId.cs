using System;
using EasyRent.EventSourcing;

namespace EasyRent.Reservation.Domain.PlaceReservation.ValueObjects.Ids;

public record TenantId(Guid Value) : EntityId(Value);