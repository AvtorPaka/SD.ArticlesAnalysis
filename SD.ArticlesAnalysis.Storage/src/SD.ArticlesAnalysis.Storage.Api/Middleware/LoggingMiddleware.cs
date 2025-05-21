using System.Text;
using SD.ArticlesAnalysis.Storage.Api.Extensions;

namespace SD.ArticlesAnalysis.Storage.Api.MiddleWare;

internal class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate requestDelegate)
    {
        _next = requestDelegate;
    }
    
    public async Task InvokeAsync(HttpContext context, ILogger<LoggingMiddleware> logger, IWebHostEnvironment environment)
    {
        logger.LogRequestStart(
            curTime: DateTime.UtcNow,
            callId: context.TraceIdentifier,
            endpointRoute: context.Request.Path
        );

        if (environment.IsDevelopment())
        {
            logger.LogRequestHeaders(
                curTime: DateTime.UtcNow,
                callId: context.TraceIdentifier,
                headers: QueryRequestHeader(context.Request)
            );
        }

        await _next.Invoke(context);

        logger.LogRequestEnd(
            curTime: DateTime.UtcNow,
            callId: context.TraceIdentifier,
            endpointRoute: context.Request.Path
        );
    }
    
    private string QueryRequestHeader(HttpRequest request)
    {
        StringBuilder headerMetaBuilder = new StringBuilder();

        foreach (var headerMeta in request.Headers)
        {
            headerMetaBuilder.Append($">>header: {headerMeta.Key}, value: {headerMeta.Value}\n");
        }

        return headerMetaBuilder.ToString();
    }
}