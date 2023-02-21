using System;

namespace EasyRent.RentalAd.Requests.V1.RentalAd;

public record UpdateRentalAdAddressRequest(Guid RentalAdId,
    string NewCountry,
    string NewCity,
    string NewStreet);