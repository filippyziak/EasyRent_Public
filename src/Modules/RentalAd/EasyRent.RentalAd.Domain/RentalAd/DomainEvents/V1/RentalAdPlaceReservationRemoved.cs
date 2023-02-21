using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceReservationRemoved(
    Guid RentalAdId,
    Guid PlaceReservationId
) : IDomainEvent;