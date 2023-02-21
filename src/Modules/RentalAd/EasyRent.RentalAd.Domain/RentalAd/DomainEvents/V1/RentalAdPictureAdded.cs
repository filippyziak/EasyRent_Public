﻿using System;
using EasyRent.EventSourcing;

namespace EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

public record RentalAdPictureAdded(
    Guid RentalAdId,
    Guid PlacePictureId,
    string PlacePictureUrl
) : IDomainEvent;