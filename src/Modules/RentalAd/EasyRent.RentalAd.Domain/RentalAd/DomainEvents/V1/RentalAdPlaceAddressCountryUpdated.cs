using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlaceAddressCountryUpdated(
    Guid RentalAdId,
    Guid PlaceAddressId,
    string NewCountry,
    string NewCity,
    string NewStreet
) : IDomainEvent;