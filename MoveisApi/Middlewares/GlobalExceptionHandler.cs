using Microsoft.AspNetCore.Diagnostics;

namespace MoveisApi.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        
            private readonly ILogger<GlobalExceptionHandler> _logger;
            private readonly IWebHostEnvironment _env;

            public GlobalExceptionHandler(
                ILogger<GlobalExceptionHandler> logger,
                IWebHostEnvironment env)
            {
                _logger = logger;
                _env = env;
            }

            public async ValueTask<bool> TryHandleAsync(
                HttpContext httpContext,
                Exception exception,
                CancellationToken cancellationToken)
            {
                int statusCode = exception switch
                {
                    BusinessException be => be.StatusCode,
                    ArgumentException => StatusCodes.Status400BadRequest,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError
                };

                _logger.LogError(
                    exception,
                    "Exception | TraceId: {TraceId} | Message: {Message}",
                    httpContext.TraceIdentifier,
                    exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Title = "Request Failed",
                    Status = statusCode,
                    Type = $"https://httpstatuses.com/{statusCode}",
                    Instance = httpContext.Request.Path
                };

                if (_env.IsDevelopment())
                {
                    problemDetails.Detail = exception.Message;
                    problemDetails.Extensions["stackTrace"] = exception.StackTrace;
                }
                else
                {
                    problemDetails.Detail = "An unexpected error occurred.";
                }

                problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

                httpContext.Response.StatusCode = statusCode;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

                return true;
            }
        }
    }

