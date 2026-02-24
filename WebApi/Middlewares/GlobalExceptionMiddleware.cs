using CarShopFinal.Domain.NotFoundException;
using Microsoft.AspNetCore.Mvc;

namespace CarShopFinal.WebApi.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
            _logger.LogError(ex, ex.Message);
            await handleExceptionAsync(context, ex);
            
        }
    }

    private static async Task handleExceptionAsync(HttpContext context, Exception exception)
    {
        var k = exception switch
        {
            NotFoundException => new ProblemDetails
            { 
                Status = StatusCodes.Status404NotFound,
                Title = "Object not Found",
                Detail = exception.Message
            },
            
            ApplicationException => new ProblemDetails
            {
            
                Status = StatusCodes.Status500InternalServerError
            
            },
            
            _ => new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Test",
                Detail = "Uninspected error"
            }
        };
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = k.Status!.Value;
        await context.Response.WriteAsJsonAsync(k);
    }
}