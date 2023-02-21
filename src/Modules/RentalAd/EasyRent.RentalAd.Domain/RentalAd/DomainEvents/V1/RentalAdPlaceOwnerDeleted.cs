using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceOwnerDeleted(
    Guid RentalAdId,
    Guid PlaceOwnerId
) : IDomainEvent;