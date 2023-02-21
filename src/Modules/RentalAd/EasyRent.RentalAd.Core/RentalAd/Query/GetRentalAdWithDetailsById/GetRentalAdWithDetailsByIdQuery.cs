using System;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAdWithDetailsById;

public record GetRentalAdWithDetailsByIdQuery(Guid RentalAdId) : IRequest<GetRentalAdWithDetailsByIdResponse>;