using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdDescriptionUpdated(
    Guid RentalAdId,
    string Description
) : IDomainEvent;