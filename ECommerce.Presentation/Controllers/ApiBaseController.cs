using ECommerce.Shared.CommonResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        // Handle Result without value
        // if Result.Success with no content 204
        // if Result.Failure Return problem details along with desc , status code
        protected IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
                return NoContent();
            else
                return HandleProblem(result.Errors);
        }


        // Handle Result with value
        // if Result.Success with value return 200 along with value
        // if Result.Failure Return problem details along with desc , status code
        protected ActionResult<TValue> HandleResult<TValue>(Result<TValue> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return HandleProblem(result.Errors);
        }

        private ActionResult HandleProblem(IReadOnlyList<Error> errors)
        {
            // If No Errors are Provided, Return 500
            if (errors.Count == 0)
                return Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "An unexpected error occurred");

            // If All Errors are Validation Errors, Handel them as Validation Problem
            if (errors.All(e => e.ErrorType == ErrorType.Validation))
                return HandleValidationErrors(errors);

            // If There is only one error , Handle as single error Problem

            return HandleSingleError(errors[0]);
        }

        private ActionResult HandleSingleError(Error error)
        {
            return Problem(
                title: error.Code,
                detail: error.Description,
                type: error.ErrorType.ToString(),
                statusCode: MapErrorTypeToStatusCode(error.ErrorType)
                );
        }

        private static int MapErrorTypeToStatusCode(ErrorType errorType)
        => errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.InvalidCredentials => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        private ActionResult HandleValidationErrors(IReadOnlyList<Error> errors)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem(modelState);
        }
    }
}
