using System.Text.Json;

namespace LibraryHub.WebApi.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Beklenmeyen bir hata oluştu. {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode =
                    StatusCodes.Status500InternalServerError;

                context.Response.ContentType = "application/json";

                var response = new
                {
                    statusCode = 500,
                    message = "Beklenmeyen bir hata oluştu."
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
        }
    }
}