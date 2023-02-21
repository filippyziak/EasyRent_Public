using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceAddressStreetUpdated(
    Guid RentalAdId,
    Guid PlaceAddressId,
    string NewStreet
) : IDomainEvent;