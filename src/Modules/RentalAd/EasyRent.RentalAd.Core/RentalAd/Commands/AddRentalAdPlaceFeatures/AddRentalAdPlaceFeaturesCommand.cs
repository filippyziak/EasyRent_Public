using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.AddRentalAdPlaceFeatures;

public record AddRentalAdPlaceFeaturesCommand : IRequest<AddRentalAdPlaceFeaturesResponse>
{
    public Guid RentalAdId { get; init; }
    public IReadOnlyList<Guid> AddedFeatureIds { get; init; } = ImmutableList<Guid>.Empty;
}