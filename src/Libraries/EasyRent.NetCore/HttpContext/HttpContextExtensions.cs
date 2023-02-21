using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace EasyRent.NetCore.HttpContext;

public static class HttpContextExtensions
{
    public static void AddApplicationError(this HttpResponse response, string errorCode)
    {
        const string errorCodeHeader = "Error-Code";

        response.Headers.Add(errorCodeHeader, errorCode);
        response.AddAccessControlExposeHeaders(errorCodeHeader);
        response.Headers.Add(HeaderNames.AccessControlAllowOrigin, "*");
    }

    public static void AddAccessControlExposeHeaders(this HttpResponse response, params string[] headers)
        => response.Headers.Add(HeaderNames.AccessControlExposeHeaders, string.Join(", ", headers));
}