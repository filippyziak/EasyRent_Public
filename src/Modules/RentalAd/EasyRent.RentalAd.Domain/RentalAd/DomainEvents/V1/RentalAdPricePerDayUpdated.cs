using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPricePerDayUpdated(
    Guid RentalAdId,
    decimal PricePerDay
) : IDomainEvent;