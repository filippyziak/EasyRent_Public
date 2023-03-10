using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPlacePictureRemoved(Guid RentalAdId, Guid PictureId) : IDomainEvent;