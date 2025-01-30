using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Sales.API.Middlewares;

public class GlobalExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            ProblemDetails problemDetails = new()
            {
                Title = "An internal server error occurred while processing your request",
                Detail = ex.Message
            };

            string json = JsonSerializer.Serialize(problemDetails);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(json);
        }
    }
}

