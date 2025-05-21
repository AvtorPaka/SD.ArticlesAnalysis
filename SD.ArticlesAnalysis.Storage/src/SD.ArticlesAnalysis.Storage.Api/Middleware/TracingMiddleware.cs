using Microsoft.IdentityModel.Tokens;

namespace SD.ArticlesAnalysis.Storage.Api.MiddleWare;

public class TracingMiddleware
{
    private readonly RequestDelegate _next;

    public TracingMiddleware(RequestDelegate requestDelegate)
    {
        _next = requestDelegate;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        string? iscTraceId = context.Request.Headers["X-Trace-Id"];
        context.TraceIdentifier = string.IsNullOrEmpty(iscTraceId) ? Guid.NewGuid().ToString() : iscTraceId;
        context.Response.Headers["X-Trace-Id"] = context.TraceIdentifier;
        await _next.Invoke(context);
    }
}