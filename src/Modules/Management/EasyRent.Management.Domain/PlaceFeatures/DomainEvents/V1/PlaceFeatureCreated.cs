using System;
using EasyRent.EventSourcing;

namespace EasyRent.Management.Domain.PlaceFeatures.DomainEvents.V1;

public record PlaceFeatureCreated(Guid PlaceFeatureId,
    string Description,
    string PictureUrl) : IDomainEvent;