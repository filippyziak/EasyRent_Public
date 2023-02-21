using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceAddressCityUpdated(
    Guid RentalAdId,
    Guid PlaceAddressId,
    string NewCity,
    string NewStreet
) : IDomainEvent;