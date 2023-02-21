using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceFeatureRemoved(
    Guid RentalAdId,
    Guid PlaceFeatureId
) : IDomainEvent;