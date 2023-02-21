using System;
using System.Threading.Tasks;
using AutoMapper;
using EasyRent.Identity.Shared.Constants;
using EasyRent.NetCore.Controller;
using EasyRent.NetCore.HttpContext;
using EasyRent.Reservation.Core.Reservation.Commands.CancelPlaceReservation;
using EasyRent.Reservation.Core.Reservation.Commands.CreatePlaceReservation;
using EasyRent.Reservation.Core.Reservation.Commands.ReviewPlaceReservation;
using EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForRentalAd;
using EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForTenant;
using EasyRent.Reservation.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyRent.Reservation.Controllers.V1;

[Authorize]
[Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
[Route("api/v1/reservation/[controller]")]
public class PlaceReservationController : BaseApiController
{
    private readonly IReadOnlyHttpAccessor _readOnlyHttpAccessor;

    public PlaceReservationController(MediatrControllerRequestHandler requestHandler,
        IMapper mapper,
        IReadOnlyHttpAccessor readOnlyHttpAccessor) : base(requestHandler, mapper)
    {
        _readOnlyHttpAccessor = readOnlyHttpAccessor;
    }

    [Authorize(Policy = nameof(AuthorizationPolicies.TenantPolicy))]
    [HttpPost]
    public Task<IActionResult> Reserve([FromBody] CreatePlaceReservationRequest request)
        => RequestHandler.HandleRequestAsync<CreatePlaceReservationCommand, CreatePlaceReservationResponse>(this,
            Mapper.Map<CreatePlaceReservationCommand>(request) with
            {
                TentantId = new Guid(_readOnlyHttpAccessor.CurrentUserId)
            });

    [Authorize(Policy = nameof(AuthorizationPolicies.TenantPolicy))]
    [HttpGet("tenant")]
    public Task<IActionResult> ReservationsForTenant()
        => RequestHandler.HandleRequestAsync<GetReservationsForTenantQuery, GetReservationsForTenantResponse>(this,
            new GetReservationsForTenantQuery(new Guid(_readOnlyHttpAccessor.CurrentUserId)));

    [AllowAnonymous]
    [HttpGet("rentalAd")]
    public Task<IActionResult> ReservationsForRentalAd(string rentalAdId)
        => RequestHandler.HandleRequestAsync<GetReservationsForRentalAdQuery, GetReservationsForRentalAdResponse>(this,
            new GetReservationsForRentalAdQuery(new Guid(rentalAdId)));

    [Authorize(Policy = nameof(AuthorizationPolicies.TenantPolicy))]
    [HttpPost("review")]
    public Task<IActionResult> Review([FromBody] ReviewPlaceReservationRequest request)
        => RequestHandler.HandleRequestAsync<ReviewPlaceReservationCommand, ReviewPlaceReservationResponse>(this,
            Mapper.Map<ReviewPlaceReservationCommand>(request) with
            {
                TentantId = new Guid(_readOnlyHttpAccessor.CurrentUserId)
            });

    [HttpDelete("cancel")]
    public Task<IActionResult> Cancel([FromQuery] Guid placeReservationId)
        => RequestHandler.HandleRequestAsync<CancelPlaceReservationCommand, CancelPlaceReservationResponse>(this,
            new CancelPlaceReservationCommand
            {
                PlaceReservationId = placeReservationId,
                CurrentAccountId = new Guid(_readOnlyHttpAccessor.CurrentUserId)
            });
}