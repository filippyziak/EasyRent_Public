using System;

namespace EasyRent.RentalAd.Requests.V1.RentalAd;

public record SetRentalAdMainPictureRequest(Guid RentalAdId, Guid RentalAdMainPictureId);