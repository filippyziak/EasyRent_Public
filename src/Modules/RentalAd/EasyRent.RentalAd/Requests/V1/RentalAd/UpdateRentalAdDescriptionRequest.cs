using System;

namespace EasyRent.RentalAd.Requests.V1.RentalAd;

public record UpdateRentalAdDescriptionRequest(Guid RentalAdId, string NewDescription);