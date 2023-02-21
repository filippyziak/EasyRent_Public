using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceFeaturePictureUrlUpdated(
    Guid RentalAdId,
    Guid PlaceFeatureId,
    string NewPictureUrl
) : IDomainEvent;