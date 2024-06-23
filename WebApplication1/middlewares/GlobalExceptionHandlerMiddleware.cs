using System.Net;
using System.Text.Json;
using WebApplication1.Models.common;

namespace WebApplication1.middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public async Task InvokeAsync(HttpContext context)
    {
        string? exceptionMessage = null;
        int? statusCode = null;
        try
        {
            await next(context);
            if (context.Response.StatusCode == (int)HttpStatusCode.MethodNotAllowed)
            {
                exceptionMessage = $"{context.Request.Method} is not allowed";
                statusCode = (int)HttpStatusCode.MethodNotAllowed;
                throw new Exception();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, exceptionMessage, statusCode);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, string? exceptionMessage, int? statusCode)
    {
        context.Response.Clear();
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var errorResponse = new ApiResponse<dynamic>(new DataBaseResult<dynamic>(
            data: null,
            success: false,
            message: exceptionMessage ?? "Something went wrong",
            statusCode: statusCode ?? 500
        ));
        context.Response.StatusCode = statusCode ?? 500;
        
        var jsonResponse = JsonSerializer.Serialize(errorResponse.Value,JsonOptions);

        return context.Response.WriteAsync(jsonResponse);
    }
}