using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace EasyRent.NetCore.HttpContext;

public class HttpAccessor : IReadOnlyHttpAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpAccessor(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;


    public string CurrentUserId
        => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

    public string CurrentUserEmailAddress
        => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

    public string CurrentUserAccountType
        => _httpContextAccessor.HttpContext.User.FindFirstValue("AccountType");
}