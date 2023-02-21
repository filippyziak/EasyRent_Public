using System;
using System.Threading.Tasks;
using AutoMapper;
using EasyRent.Identity.Core.Handlers.Command.LoginAccount;
using EasyRent.Identity.Core.Handlers.Command.RegisterAccount;
using EasyRent.Identity.Core.Handlers.Command.UpdateAccountEmailAddress;
using EasyRent.Identity.Core.Handlers.Command.UpdateAccountPassword;
using EasyRent.Identity.Core.Handlers.Query.GetAccountById;
using EasyRent.Identity.Core.Handlers.Query.GetAccounts;
using EasyRent.Identity.Requests;
using EasyRent.Identity.Shared.Constants;
using EasyRent.NetCore.Controller;
using EasyRent.NetCore.HttpContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyRent.Identity.Controllers.V1;

[Route("api/v1/identity/[controller]")]
public class AccountController : BaseApiController
{
    private readonly IReadOnlyHttpAccessor _httpAccessor;

    public AccountController(MediatrControllerRequestHandler requestHandler,
        IMapper mapper,
        IReadOnlyHttpAccessor httpAccessor) : base(requestHandler, mapper)
    {
        _httpAccessor = httpAccessor;
    }

    [HttpPost]
    public Task<IActionResult> RegisterAccount([FromBody] RegisterAccountRequest request)
        => RequestHandler.HandleRequestAsync<RegisterAccountCommand, RegisterAccountResponse>(this,
            Mapper.Map<RegisterAccountCommand>(request));

    [HttpPost("login")]
    public Task<IActionResult> RegisterAccount([FromBody] LoginAccountRequest request)
        => RequestHandler.HandleRequestAsync<LoginAccountCommand, LoginAccountResponse>(this,
            Mapper.Map<LoginAccountCommand>(request));

    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [HttpPatch("email")]
    public Task<IActionResult> UpdateAccountEmailAddress([FromBody] UpdateAccountEmailAddressRequest request)
        => RequestHandler.HandleRequestAsync<UpdateAccountEmailAddressCommand, UpdateAccountEmailAddressResponse>(this,
            Mapper.Map<UpdateAccountEmailAddressCommand>(request) with
            {
                AccountId = new Guid(_httpAccessor.CurrentUserId)
            });

    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [HttpPatch("password")]
    public Task<IActionResult> UpdateAccountPassword([FromBody] UpdateAccountPasswordRequest request)
        => RequestHandler.HandleRequestAsync<UpdateAccountPasswordCommand, UpdateAccountPasswordResponse>(this,
            Mapper.Map<UpdateAccountPasswordCommand>(request) with
            {
                AccountId = new Guid(_httpAccessor.CurrentUserId)
            });
    
    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [HttpGet]
    public Task<IActionResult> GetAccounts(GetAccountsQuery query)
        => RequestHandler.HandleRequestAsync<GetAccountsQuery, GetAccountsResponse>(this, query);
    
    [Authorize]
    [Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
    [HttpGet("{AccountId}")]
    public Task<IActionResult> GetAccounts(Guid accountId)
        => RequestHandler.HandleRequestAsync<GetAccountByIdQuery, GetAccountByIdResponse>(this, new GetAccountByIdQuery(accountId));
}