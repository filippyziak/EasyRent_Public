using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

public record PlaceOwnerId(Guid Value) : EntityId(Value);