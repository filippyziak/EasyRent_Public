using System;

namespace EasyRent.RentalAd.Requests.V1.RentalAd;

public record RemoveRentalAdPictureRequest(Guid RentalAdId, Guid PictureId);