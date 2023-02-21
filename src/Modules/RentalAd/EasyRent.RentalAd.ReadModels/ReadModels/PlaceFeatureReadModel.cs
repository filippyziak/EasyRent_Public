using System;

namespace EasyRent.RentalAd.ReadModels.ReadModels;

public record PlaceFeatureReadModel(
    Guid PlaceFeatureId,
    string PlaceFeatureDescription,
    string PlaceFeatureUrl
);