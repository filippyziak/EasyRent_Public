using System;
using EasyRent.Shared.Pagination;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForRentalAd;

public record GetReservationsForRentalAdQuery(Guid RentalAdId) : PaginationQuery, IRequest<GetReservationsForRentalAdResponse>;