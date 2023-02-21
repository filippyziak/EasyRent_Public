using System;

namespace EasyRent.Management.ReadModels.ReadModels;

public record PlaceFeatureReadModel
{
    public Guid PlaceFeatureId { get; init; }
    public string Description { get; init; }
    public string PictureUrl { get; init; }
}