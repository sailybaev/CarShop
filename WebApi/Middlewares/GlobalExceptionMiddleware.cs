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
        var problem = exception switch
        {
            NotFoundException => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = exception.Message
            },

            InvalidOperationException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid Operation",
                Detail = exception.Message
            },

            ApplicationException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Application Error",
                Detail = exception.Message
            },

            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred."
            }
        };
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problem.Status!.Value;
        await context.Response.WriteAsJsonAsync(problem);
    }
}