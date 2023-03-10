using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceReservationReviewUpdated(
    Guid RentalAdId,
    Guid PlaceReservationId,
    string review
) : IDomainEvent;