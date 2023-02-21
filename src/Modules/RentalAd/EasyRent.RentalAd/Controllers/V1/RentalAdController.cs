using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using EasyRent.Identity.Shared.Constants;
using EasyRent.NetCore.Controller;
using EasyRent.NetCore.HttpContext;
using EasyRent.RentalAd.Core.RentalAd.Commands.AddRentalAddPictures;
using EasyRent.RentalAd.Core.RentalAd.Commands.CreateRentalAd;
using EasyRent.RentalAd.Core.RentalAd.Commands.DeleteRentalAd;
using EasyRent.RentalAd.Core.RentalAd.Commands.RemoveRentalAdPicture;
using EasyRent.RentalAd.Core.RentalAd.Commands.SetRentalAdMainPicture;
using EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdAddress;
using EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdDescription;
using EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdPricePerDay;
using EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdTitle;
using EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAds;
using EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAdsForPlaceOwner;
using EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAdWithDetailsById;
using EasyRent.RentalAd.Requests.V1.RentalAd;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyRent.RentalAd.Controllers.V1;

[Route("api/v1/rental-ad/[controller]")]
public class RentalAdController : BaseApiController
{
    private readonly IReadOnlyHttpAccessor _readOnlyHttpAccessor;

    public RentalAdController(MediatrControllerRequestHandler requestHandler,
        IMapper mapper,
        IReadOnlyHttpAccessor readOnlyHttpAccessor) : base(requestHandler, mapper)
    {
        _readOnlyHttpAccessor = readOnlyHttpAccessor;
    }

    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.PlaceOwnerPolicy))]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [HttpPost]
    public Task<IActionResult> CreateRentalAd([FromForm] CreateRentalAdRequest request, [FromQuery] decimal pricePerDay)
        => RequestHandler.HandleRequestAsync<CreateRentalAdCommand, CreateRentalAdResponse>(this,
            Mapper.Map<CreateRentalAdCommand>(request) with
            {
                PlaceOwnerId = new Guid(_readOnlyHttpAccessor.CurrentUserId),
                PricePerDay = pricePerDay
            });


    [HttpGet]
    public Task<IActionResult> GetRentalAds([FromQuery] GetRentalAdsQuery request)
        => RequestHandler.HandleRequestAsync<GetRentalAdsQuery, GetRentalAdsResponse>(this,
            Mapper.Map<GetRentalAdsQuery>(request));

    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.PlaceOwnerPolicy))]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [HttpGet("owner")]
    public Task<IActionResult> GetRentalAds()
        => RequestHandler.HandleRequestAsync<GetRentalAdsForPlaceOwnerQuery, GetRentalAdsForPlaceOwnerResponse>(this,
            new GetRentalAdsForPlaceOwnerQuery(new Guid(_readOnlyHttpAccessor.CurrentUserId)));

    [HttpGet("{rentalAdId}")]
    public Task<IActionResult> GetRentalAd(Guid rentalAdId)
        => RequestHandler.HandleRequestAsync<GetRentalAdWithDetailsByIdQuery, GetRentalAdWithDetailsByIdResponse>(this,
            new GetRentalAdWithDetailsByIdQuery(rentalAdId));

    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [Authorize(Policy = nameof(AuthorizationPolicies.PlaceOwnerPolicy))]
    [HttpPost("{rentalAdId}/pictures")]
    public Task<IActionResult> AddRentalAddPictures(Guid rentalAdId, [FromForm] IReadOnlyList<IFormFile> pictures)
        => RequestHandler.HandleRequestAsync<AddRentalAdPicturesCommand, AddRentalAdPicturesResponse>(this,
            new AddRentalAdPicturesCommand
            {
                RentalAdId = rentalAdId,
                PictureFiles = pictures,
                CurrentOwnerId = new Guid(_readOnlyHttpAccessor.CurrentUserId)
            });

    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [Authorize(Policy = nameof(AuthorizationPolicies.PlaceOwnerPolicy))]
    [HttpPatch("picture/main")]
    public Task<IActionResult> SetRentalAdMainPicture([FromBody] SetRentalAdMainPictureRequest request)
        => RequestHandler.HandleRequestAsync<SetRentalAdMainPictureCommand, SetRentalAdMainPictureResponse>(this,
            Mapper.Map<SetRentalAdMainPictureCommand>(request) with
            {
                CurrentOwnerId = new Guid(_readOnlyHttpAccessor.CurrentUserId)
            });

    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [Authorize(Policy = nameof(AuthorizationPolicies.PlaceOwnerPolicy))]
    [HttpPatch("description")]
    public Task<IActionResult> UpdateRentalAdDescription([FromBody] UpdateRentalAdDescriptionRequest request)
        => RequestHandler.HandleRequestAsync<UpdateRentalAdDescriptionCommand, UpdateRentalAdDescriptionResponse>(this,
            Mapper.Map<UpdateRentalAdDescriptionCommand>(request) with
            {
                CurrentOwnerId = new Guid(_readOnlyHttpAccessor.CurrentUserId)
            });

    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [Authorize(Policy = nameof(AuthorizationPolicies.PlaceOwnerPolicy))]
    [HttpPatch("price")]
    public Task<IActionResult> UpdateRentalAdPricePerDay([FromBody] UpdateRentalAdPricePerDayRequest request)
        => RequestHandler.HandleRequestAsync<UpdateRentalAdPricePerDayCommand, UpdateRentalAdPricePerDayResponse>(this,
            Mapper.Map<UpdateRentalAdPricePerDayCommand>(request) with
            {
                CurrentOwnerId = new Guid(_readOnlyHttpAccessor.CurrentUserId)
            });

    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [Authorize(Policy = nameof(AuthorizationPolicies.PlaceOwnerPolicy))]
    [HttpPatch("title")]
    public Task<IActionResult> UpdateRentalAdTitle([FromBody] UpdateRentalAdTitleRequest request)
        => RequestHandler.HandleRequestAsync<UpdateRentalAdTitleCommand, UpdateRentalAdTitleResponse>(this,
            Mapper.Map<UpdateRentalAdTitleCommand>(request) with
            {
                CurrentOwnerId = new Guid(_readOnlyHttpAccessor.CurrentUserId)
            });

    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [Authorize(Policy = nameof(AuthorizationPolicies.PlaceOwnerPolicy))]
    [HttpPatch("address")]
    public Task<IActionResult> UpdateRentalAdAddress([FromBody] UpdateRentalAdAddressRequest request)
        => RequestHandler.HandleRequestAsync<UpdateRentalAdAddressCommand, UpdateRentalAdAddressResponse>(this,
            Mapper.Map<UpdateRentalAdAddressCommand>(request) with
            {
                CurrentOwnerId = new Guid(_readOnlyHttpAccessor.CurrentUserId)
            });

    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [Authorize(Policy = nameof(AuthorizationPolicies.PlaceOwnerPolicy))]
    [HttpDelete("picture")]
    public Task<IActionResult> RemoveRentalAdPicture([FromQuery] RemoveRentalAdPictureRequest request)
        => RequestHandler.HandleRequestAsync<RemoveRentalAdPictureCommand, RemoveRentalAdPictureResponse>(this,
            Mapper.Map<RemoveRentalAdPictureCommand>(request) with
            {
                CurrentOwnerId = new Guid(_readOnlyHttpAccessor.CurrentUserId)
            });
    
    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [Authorize(Policy = nameof(AuthorizationPolicies.PlaceOwnerPolicy))]
    [HttpDelete]
    public Task<IActionResult> RemoveRentalAd([FromQuery] Guid rentalAdId)
        => RequestHandler.HandleRequestAsync<DeleteRentalAdCommand, DeleteRentalAdResponse>(this,
            new DeleteRentalAdCommand(rentalAdId));
}