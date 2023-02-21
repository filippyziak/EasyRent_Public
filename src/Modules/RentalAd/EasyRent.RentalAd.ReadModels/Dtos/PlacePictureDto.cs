using System;

namespace EasyRent.RentalAd.ReadModels.Dtos;

public record PlacePictureDto
{
    public Guid PlacePictureId { get; init; }
    public string PictureUrl { get; init; }
    public string PlacePictureState { get; init; }
}