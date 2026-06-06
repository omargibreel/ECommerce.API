using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResult(ActionContext actionContext)
        {
            var errors = actionContext.ModelState
                .Where(ms => ms.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            var problemDetails = new ValidationProblemDetails(errors)
            {
                Title = "Validation Failed",
                Detail = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Instance = actionContext.HttpContext.Request.Path
            };

            return new BadRequestObjectResult(problemDetails);
        }
    }
}