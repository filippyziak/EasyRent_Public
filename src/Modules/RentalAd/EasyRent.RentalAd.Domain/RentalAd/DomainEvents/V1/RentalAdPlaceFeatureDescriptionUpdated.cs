using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceFeatureDescriptionUpdated(
    Guid RentalAdId,
    Guid PlaceFeatureId,
    string NewDescription
) : IDomainEvent;