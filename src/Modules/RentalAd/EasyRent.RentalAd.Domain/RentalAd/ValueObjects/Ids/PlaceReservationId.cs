using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

public record PlaceReservationId(Guid Value) : EntityId(Value);