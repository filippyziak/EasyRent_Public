using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EasyRent.RentalAd.Requests.V1.RentalAd;

public record CreateRentalAdRequest
(
    string Description,
    string Title,
    string Country,
    string City,
    string Street,
    IReadOnlyList<IFormFile> PictureFiles
);