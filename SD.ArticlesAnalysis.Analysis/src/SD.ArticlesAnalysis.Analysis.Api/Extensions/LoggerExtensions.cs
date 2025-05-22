using System.Net;

namespace SD.ArticlesAnalysis.Analysis.Api.Extensions;

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
        Message =
            "[{CallId}] [{CurTime}] Article with id: {ArticleId} data could not be found on SD.AA.Storage service. Check SD.AA.Storage inner logs for trace-id: {TraceId} if needed."
    )]
    public static partial void LogArticleNotFoundInStorage(this ILogger logger,
        string callId,
        DateTime curTime,
        long articleId,
        string? traceId
    );

    [LoggerMessage(
        Level = LogLevel.Error,
        EventId = 4002,
        Message =
            "[{CallId}] [{CurTime}] Unexpected response from SD.AA.Storage service for address: {Address} with status-code: {StatusCodeInt} - {StatusCode}. Check SD.AA.Storage inner logs for trace-id: {TraceId}"
    )]
    public static partial void LogStorageServiceUnexpectedResponse(this ILogger logger,
        string callId,
        DateTime curTime,
        HttpStatusCode statusCode,
        Uri? address,
        int statusCodeInt,
        string? traceId
    );

    [LoggerMessage(
        Level = LogLevel.Error,
        EventId = 4003,
        Message = "[{CallId}] [{CurTime}] Unable to get the response from SD.AA.Storage service for address: {Address}"
    )]
    public static partial void LogStorageServiceUnavailable(
        this ILogger logger,
        string callId,
        DateTime curTime,
        Uri? address);


    [LoggerMessage(
        Level = LogLevel.Error,
        EventId = 4004,
        Message =
            "[{CallId}] [{CurTime}] Unexpected response from Word Cloud api for address: {Address} with status-code: {StatusCodeInt} - {StatusCode}."
    )]
    public static partial void LogWordCloudApiUnexpectedResponse(this ILogger logger,
        string callId,
        DateTime curTime,
        HttpStatusCode statusCode,
        Uri? address,
        int statusCodeInt
    );

    [LoggerMessage(
        Level = LogLevel.Error,
        EventId = 4005,
        Message = "[{CallId}] [{CurTime}] Unable to get the response from Word Cloud api for address: {Address}"
    )]
    public static partial void LogWordCloudApuUnavailable(
        this ILogger logger,
        string callId,
        DateTime curTime,
        Uri? address
    );

    [LoggerMessage(
        Level = LogLevel.Error,
        EventId = 4006,
        Message =
            "[{CallId}] [{CurTime}] Analysis for article with id: {ArticleId} could not be found."
    )]
    public static partial void LogArticleAnalysisNotFound(this ILogger logger,
        string callId,
        DateTime curTime,
        long articleId
    );
    
    [LoggerMessage(
        Level = LogLevel.Error,
        EventId = 4007,
        Message =
            "[{CallId}] [{CurTime}] Word Cloud for article with id: {ArticleId} could not be found."
    )]
    public static partial void LogArticleWordCloudNotFound(this ILogger logger,
        string callId,
        DateTime curTime,
        long articleId
    );
    
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