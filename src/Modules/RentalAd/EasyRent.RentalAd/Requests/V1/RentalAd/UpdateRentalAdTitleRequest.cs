using System;

namespace EasyRent.RentalAd.Requests.V1.RentalAd;

public record UpdateRentalAdTitleRequest(Guid RentalAdId, string NewTitle);