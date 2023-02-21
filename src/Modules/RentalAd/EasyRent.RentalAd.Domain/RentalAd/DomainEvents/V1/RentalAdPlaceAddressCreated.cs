using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceAddressCreated(
    Guid RentalAdId,
    Guid PlaceAddressId,
    string Country,
    string City,
    string Street
) : IDomainEvent;