using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EasyRent.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyRent.NetCore.Filters;

public class ValidationFilter : ActionFilterAttribute
{
    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(e => e.Value.Errors.Any())
                .ToDictionary(kv => kv.Key,
                    e => e.Value.Errors
                        .Select(me => me.ErrorMessage));

            var validationResult = BaseResponse.ValidationFailure(errors);

            context.Result = new JsonResult(validationResult) { StatusCode = (int)HttpStatusCode.InternalServerError };
            return Task.CompletedTask;
        }

        return next();
    }
}