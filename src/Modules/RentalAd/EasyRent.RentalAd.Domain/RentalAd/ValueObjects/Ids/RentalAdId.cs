using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

public record RentalAdId(Guid Value) : AggregateId(Value);