using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceOwnerEmailAddressUpdated(
    Guid RentalAdId,
    Guid PlaceOwnerId,
    string NewEmailAddress
) : IDomainEvent;