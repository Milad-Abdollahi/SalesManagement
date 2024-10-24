using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SalesManagementLibrary.Exceptions;

namespace SalesManagementApi.ExceptionHandling;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DuplicateRecordException ex)
        {
            await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (GenericException ex)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.InternalServerError,
                ex.Message,
                ex.InnerException?.Message
            );
        }
        catch (Exception ex)
        {
            // Todo: comment to see if the following if block is actually neccessary!
            if (context.Response.HasStarted)
            {
                throw;
            }

            await HandleExceptionAsync(
                context,
                HttpStatusCode.InternalServerError,
                "An Unexpected Error Occured: " + ex.Message,
                ex.InnerException?.Message
            );
        }
    }

    private static Task HandleExceptionAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string message,
        string? details = null
    )
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var errorResponse = new ErrorResponse
        {
            Type = "Error",
            Title = message,
            Status = context.Response.StatusCode,
            Errors = new Dictionary<string, string[]>
            {
                { "Message", new[] { message } },
                { "Details", details is not null ? new[] { details } : new string[0] }
            },
            TraceId = context.TraceIdentifier,
        };

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var result = JsonSerializer.Serialize(errorResponse, options);
        return context.Response.WriteAsync(result);
    }
}
