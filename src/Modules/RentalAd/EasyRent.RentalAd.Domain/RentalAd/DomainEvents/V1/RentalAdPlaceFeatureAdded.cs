using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceFeatureAdded(
    Guid RentalAdId,
    Guid PlaceFeatureId,
    string Description,
    string PictureUrl
) : IDomainEvent;