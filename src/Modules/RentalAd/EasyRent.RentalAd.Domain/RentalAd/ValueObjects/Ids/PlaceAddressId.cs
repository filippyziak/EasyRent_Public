using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

public record PlaceAddressId(Guid Value) : EntityId(Value);