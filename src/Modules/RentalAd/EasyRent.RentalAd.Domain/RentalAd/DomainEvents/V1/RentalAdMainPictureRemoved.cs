using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdMainPictureRemoved(Guid RentalAdId, Guid PictureId) : IDomainEvent;