using Humanizer;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UMaTLMS.API.Attributes;

public class ValidationErrorFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        //
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                            .SelectMany(v => v.Errors)
                            .Select(v => v.ErrorMessage)
                            .ToList();

            var responseObj = new ApiErrorResponse(404, "One or more validation errors occurred.", errors.Humanize());
            context.Result = new JsonResult(responseObj)
            {
                StatusCode = 200
            };
        }
    }
}
