using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdCreated(
    Guid RentalAdId,
    Guid PlaceOwnerId,
    Guid PlaceAddressId,
    string Title,
    string Description,
    decimal PricePerDay,
    string Country,
    string City,
    string Street
) : IDomainEvent;