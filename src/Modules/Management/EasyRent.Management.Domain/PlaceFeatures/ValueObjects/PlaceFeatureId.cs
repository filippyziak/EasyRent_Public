using System;
using EasyRent.EventSourcing;

namespace EasyRent.Management.Domain.PlaceFeatures.ValueObjects;

public record PlaceFeatureId(Guid Value) : AggregateId(Value);