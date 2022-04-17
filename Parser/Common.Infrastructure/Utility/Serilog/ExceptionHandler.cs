using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.Infrastructure.Utility.Serilog;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;

    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        WebHelper.LogWebError("NetCoreLogger.Web", "WebApi", ex, context);

        var errorId = Activity.Current?.Id ?? context.TraceIdentifier;
        var customError = $"ErrorId-{errorId}:Message-Some kind of error happened in the API.";
        var result = JsonConvert.SerializeObject(customError);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(result);
    }
}
