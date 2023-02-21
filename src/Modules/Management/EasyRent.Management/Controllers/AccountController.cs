using System;
using System.Threading.Tasks;
using AutoMapper;
using EasyRent.Identity.Shared.Constants;
using EasyRent.Management.Core.Handlers.Accounts.Command.ActivateAccount;
using EasyRent.Management.Core.Handlers.Accounts.Command.SuspendAccount;
using EasyRent.Management.Requests;
using EasyRent.NetCore.Controller;
using EasyRent.NetCore.HttpContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyRent.Management.Controllers;

[Authorize]
[Authorize(Policy = nameof(AuthorizationPolicies.SuspendPolicy))]
[Authorize(Policy = nameof(AuthorizationPolicies.ModeratorPolicy))]
[Route("api/v1/management/[controller]")]
public class AccountController : BaseApiController
{
    private readonly IReadOnlyHttpAccessor _httpAccessor;

    public AccountController(IReadOnlyHttpAccessor httpAccessor,
        MediatrControllerRequestHandler requestHandler, 
        IMapper mapper) : base(requestHandler, mapper)
    {
        _httpAccessor = httpAccessor;
    }

    [HttpPatch("suspend")]
    public Task<IActionResult> SuspendAccount([FromBody] SuspendAccountRequest request)
        => RequestHandler.HandleRequestAsync<SuspendAccountCommand, SuspendAccountResponse>(this,
            Mapper.Map<SuspendAccountCommand>(request) with
            {
                CurrentAccountId = new Guid(_httpAccessor.CurrentUserId)
            });

    [HttpPatch("activate")]
    public Task<IActionResult> ActivateAccount([FromBody] ActivateAccountRequest request)
        => RequestHandler.HandleRequestAsync<ActivateAccountCommand, ActivateAccountResponse>(this,
            Mapper.Map<ActivateAccountCommand>(request));
}