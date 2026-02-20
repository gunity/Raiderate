using Backend.Shared.Contracts;
using Backend.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Backend.Shared.Middlewares;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger
)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var traceId = context.TraceIdentifier;

            int code;
            string message;
            if (exception is AppException appException)
            {
                code = appException.Code;
                message = appException.Message;
            }
            else
            {
                code = StatusCodes.Status500InternalServerError;
                message = "Internal Server Error";
            }

            logger.LogError(exception,
                "code=({code}), trace ID=({traceID}), message=({message})",
                code, traceId, message);

            if (!context.Response.HasStarted)
            {
                context.Response.Headers["X-Trace-Id"] = traceId;
                context.Response.StatusCode = code;
                context.Response.ContentType = "application/json";

                var apiError = new ApiError(code, message, traceId);
                await context.Response.WriteAsJsonAsync(apiError);
            }
        }
    }
}