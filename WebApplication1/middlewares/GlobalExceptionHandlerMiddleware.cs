using System.Net;
using System.Text.Json;
using WebApplication1.Models.common;

namespace WebApplication1.middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.Clear();
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var errorResponse = new ApiResponse<dynamic>(new DataBaseResult<dynamic>(
            data:null,
            success:false,
            message:"Something went wrong",
            statusCode:500
            ));
        
        var jsonResponse = JsonSerializer.Serialize(errorResponse.Value);

        return context.Response.WriteAsync(jsonResponse);
    }
}