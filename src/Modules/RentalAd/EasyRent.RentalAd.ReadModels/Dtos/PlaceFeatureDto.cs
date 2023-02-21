using System;

namespace EasyRent.RentalAd.ReadModels.Dtos;

public record PlaceFeatureDto
{
    public Guid PlaceFeatureId { get; init; }
    public string Description { get; init; }
    public string PictureUrl { get; init; }
}