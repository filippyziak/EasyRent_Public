using System;
using EasyRent.Shared.Pagination;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForTenant;

public record GetReservationsForTenantQuery(Guid TenantId) : PaginationQuery, IRequest<GetReservationsForTenantResponse>;