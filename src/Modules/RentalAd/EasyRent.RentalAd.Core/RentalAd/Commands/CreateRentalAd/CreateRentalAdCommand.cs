using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.CreateRentalAd;

public record CreateRentalAdCommand : IRequest<CreateRentalAdResponse>
{
    public string Description { get; init; }
    public string Title { get; init; }
    public decimal PricePerDay { get; init; }
    public string Country { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
    public Guid PlaceOwnerId { get; init; }

    public IReadOnlyList<IFormFile> PictureFiles { get; init; } = ImmutableList<IFormFile>.Empty;
}