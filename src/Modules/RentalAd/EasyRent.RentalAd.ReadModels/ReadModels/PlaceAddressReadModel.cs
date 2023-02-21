using System;

namespace EasyRent.RentalAd.ReadModels.ReadModels;

public record PlaceAddressReadModel(
    Guid PlaceAddressId,
    string Country,
    string City,
    string Street
);