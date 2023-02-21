using System;
using System.Collections.Generic;
using System.IO;

namespace EasyRent.RentalAd.Requests.V1.RentalAd;

public record AddRentalAddPicturesRequest
{
    public Guid RentalAdId { get; init; }
    public IReadOnlyList<FileStream> Pictures { get; init; }
}