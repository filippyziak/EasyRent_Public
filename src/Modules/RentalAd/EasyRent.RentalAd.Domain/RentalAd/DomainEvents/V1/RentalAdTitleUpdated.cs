using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdTitleUpdated(
    Guid RentalAdId,
    string Title
) : IDomainEvent;