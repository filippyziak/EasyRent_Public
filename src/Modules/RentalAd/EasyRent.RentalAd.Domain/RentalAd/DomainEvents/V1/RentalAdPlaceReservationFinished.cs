using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceReservationFinished(
    Guid RentalAdId,
    Guid PlaceReservationId
) : IDomainEvent;