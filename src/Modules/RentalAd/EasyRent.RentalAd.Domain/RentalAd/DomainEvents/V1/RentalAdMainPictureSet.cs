using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdMainPictureSet(
    Guid RentalAdId,
    Guid PlacePictureId
) : IDomainEvent;