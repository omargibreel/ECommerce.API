using ECommerce.Services.Implementation.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.CustomMiddlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // call the next middleware in the pipeline
                await _next.Invoke(httpContext);

                // Handle 404 Not Found 
                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound && !httpContext.Response.HasStarted)
                {
                    var problemDetails = new ProblemDetails()
                    {
                        Title = "Endpoint Not Found",
                        Detail = $"The requested endpoint '{httpContext.Request.Path}' was not found.",
                        Status = StatusCodes.Status404NotFound,
                        Instance = httpContext.Request.Path,
                    };


                    await httpContext.Response.WriteAsJsonAsync(problemDetails);
                }

            }
            catch (Exception ex)
            {
                // logging
                _logger.LogError(
                                ex,
                                "Unhandled exception occurred while processing request {Path}",
                                httpContext.Request.Path
                            );

                // Return custom Error Response
                // httpContext.response.StatusCode = StatusCodes.Status500InternalServerError;



                var statusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError,
                };


                var title = statusCode switch
                {
                    StatusCodes.Status404NotFound => "Resource Not Found",
                    StatusCodes.Status401Unauthorized => "Unauthorized",
                    _ => "Internal Server Error",
                };

                var problemDetails = new ProblemDetails()
                {
                    Title = title,
                    Detail = _environment.IsDevelopment()
                    ? ex.Message
                    : "An unexpected error occurred",
                    Instance = httpContext.Request.Path,
                    Status = statusCode
                };

                httpContext.Response.StatusCode = problemDetails.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(problemDetails);

            }
        }
    }
}
