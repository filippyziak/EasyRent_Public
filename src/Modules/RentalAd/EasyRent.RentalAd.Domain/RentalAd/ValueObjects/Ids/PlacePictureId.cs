using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

public record PlacePictureId(Guid Value) : EntityId(Value);