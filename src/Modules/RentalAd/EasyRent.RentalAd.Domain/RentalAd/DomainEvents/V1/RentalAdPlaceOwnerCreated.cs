using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceOwnerCreated(
    Guid RentalAdId,
    Guid PlaceOwnerId,
    string EmailAddress
) : IDomainEvent;