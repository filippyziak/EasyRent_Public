using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.RemoveRentalAdPlaceFeatures;

public record RemoveRentalAdPlaceFeaturesCommand : IRequest<RemoveRentalAdPlaceFeaturesResponse>
{
    public Guid RentalAdId { get; init; }
    public IReadOnlyList<Guid> RemovedFeatureIds { get; init; } = ImmutableList<Guid>.Empty;
}