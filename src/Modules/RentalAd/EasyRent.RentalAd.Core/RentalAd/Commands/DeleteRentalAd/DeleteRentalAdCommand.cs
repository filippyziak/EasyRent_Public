using System;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.DeleteRentalAd;

public record DeleteRentalAdCommand(Guid RentalAdId) : IRequest<DeleteRentalAdResponse>;