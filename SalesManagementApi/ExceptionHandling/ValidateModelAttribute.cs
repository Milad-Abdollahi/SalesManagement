using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context
                .ModelState.Where(ms => ms.Value.Errors.Count > 0)
                .Select(ms => new
                {
                    Field = ms.Key,
                    Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                })
                .ToArray();

            var errorDetails = new
            {
                StatusCode = 400,
                Message = "Validation errors occurred.",
                Errors = errors
            };

            var errorJson = JsonSerializer.Serialize(errorDetails);

            context.Result = new ContentResult
            {
                Content = errorJson,
                ContentType = "application/json",
                StatusCode = 400
            };
        }

        base.OnActionExecuting(context);
    }
}
