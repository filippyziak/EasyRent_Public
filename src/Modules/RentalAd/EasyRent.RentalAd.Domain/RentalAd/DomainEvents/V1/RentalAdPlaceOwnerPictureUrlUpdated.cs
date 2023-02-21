using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceOwnerPictureUrlUpdated(
    Guid RentalAdId,
    Guid PlaceOwnerId,
    string NewPictureUrl
) : IDomainEvent;