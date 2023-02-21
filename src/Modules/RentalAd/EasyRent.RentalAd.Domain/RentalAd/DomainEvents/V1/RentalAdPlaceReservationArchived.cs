using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceReservationArchived(
    Guid RentalAdId,
    Guid PlaceReservationId
) : IDomainEvent;