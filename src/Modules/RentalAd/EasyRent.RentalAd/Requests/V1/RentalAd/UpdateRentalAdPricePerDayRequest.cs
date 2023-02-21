using System;

namespace EasyRent.RentalAd.Requests.V1.RentalAd;

public record UpdateRentalAdPricePerDayRequest(Guid RentalAdId, decimal NewPricePerDay);