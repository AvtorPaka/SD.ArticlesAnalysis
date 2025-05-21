namespace SD.ArticlesAnalysis.Storage.Api.Extensions;

internal static partial class LoggerExtensions
{
    #region Info
    
    [LoggerMessage(
        LogLevel.Information,
        EventId = 2000,
        Message = "[{CallId}] [{CurTime}] Start executing call. Endpoint: {EndpointRoute}"
    )]
    public static partial void LogRequestStart(this ILogger logger,
        DateTime curTime,
        string callId,
        string endpointRoute);


    [LoggerMessage(
        LogLevel.Information,
        EventId = 2001,
        Message = "[{CallId}] [{CurTime}] Ended executing call. Endpoint: {EndpointRoute}"
    )]
    public static partial void LogRequestEnd(this ILogger logger,
        DateTime curTime,
        string callId,
        string endpointRoute);
    
    #endregion

    #region Error
    
    [LoggerMessage(
        Level = LogLevel.Error,
        EventId = 4000,
        Message = "[{CallId}] [{CurTime}] Unexpected exception occured during request processing."
    )]
    public static partial void LogInternalError(this ILogger logger,
        Exception exception,
        string callId,
        DateTime curTime);
    
    
    [LoggerMessage(
        Level = LogLevel.Error,
        EventId = 4001,
        Message = "[{CallId}] [{CurTime}] Request validation error: \n{Violations}"
    )]
    public static partial void LogDomainBadRequestError(this ILogger logger,
        string callId,
        DateTime curTime,
        string violations);
    
    [LoggerMessage(
        Level = LogLevel.Error,
        EventId = 4002,
        Message = "[{CallId}] [{CurTime}] Article with id: {ArticleId} not found"
    )]
    public static partial void LogArticleMetaNotFound(this ILogger logger,
        string callId,
        DateTime curTime,
        long articleId);
    
    
    [LoggerMessage(
        Level = LogLevel.Error,
        EventId = 4003,
        Message = "[{CallId}] [{CurTime}] Article data with id: {ArticleId} not found for path: {InvalidPath}"
    )]
    public static partial void LogArticleDataNotFound(this ILogger logger,
        string callId,
        DateTime curTime,
        long articleId,
        string? invalidPath);
    
    #endregion
    
    #region Debug

    [LoggerMessage(
        LogLevel.Debug,
        EventId = 1000,
        Message = "[{CallId}] [{CurTime}] Request headers:\n{Headers}"
    )]
    public static partial void LogRequestHeaders(this ILogger logger,
        DateTime curTime,
        string callId,
        string headers);

    #endregion
}