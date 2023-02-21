using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceReservationCreated(
    Guid RentalAdId,
    Guid PlaceReservationId,
    DateTime ArrivalDate,
    DateTime DepartureDate
) : IDomainEvent;