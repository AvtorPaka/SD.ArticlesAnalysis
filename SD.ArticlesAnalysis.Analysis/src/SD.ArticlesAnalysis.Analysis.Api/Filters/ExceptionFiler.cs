using Microsoft.AspNetCore.Mvc.Filters;
using SD.ArticlesAnalysis.Analysis.Api.Extensions;
using SD.ArticlesAnalysis.Analysis.Api.Filters.Utils;
using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Domain.Articles;
using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Isc.Storage;
using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Isc.WordCloud;

namespace SD.ArticlesAnalysis.Analysis.Api.Filters;

public class ExceptionFiler : IExceptionFilter
{
    private readonly ILogger<ExceptionFiler> _logger;

    public ExceptionFiler(ILogger<ExceptionFiler> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        string callId = context.HttpContext.TraceIdentifier;

        switch (context.Exception)
        {
            case ArticleNotFoundException exception:

                _logger.LogArticleNotFoundInStorage(
                    callId: callId,
                    curTime: DateTime.UtcNow,
                    articleId: exception.InvalidArticleId,
                    traceId: exception.IscException?.TraceId
                );

                ErrorRequestHandler.HandleArticleNotFoundError(context, exception);
                break;

            case ArticleAnalysisNotFoundException exception:

                _logger.LogArticleAnalysisNotFound(
                    callId: callId,
                    curTime: DateTime.UtcNow,
                    articleId: exception.ArticleId
                );

                ErrorRequestHandler.HandleArticleAnalysisBadRequestError(context, exception);
                break;

            case ArticleWordCloudNotFoundException exception:

                _logger.LogArticleWordCloudNotFound(
                    callId: callId,
                    curTime: DateTime.UtcNow,
                    articleId: exception.ArticleId
                );

                ErrorRequestHandler.HandleArticleWordCloudNotFoundError(context, exception);
                break;

            case StorageServiceResponseException exception:

                _logger.LogStorageServiceUnexpectedResponse(
                    callId: callId,
                    curTime: DateTime.UtcNow,
                    statusCode: exception.StatusCode,
                    statusCodeInt: (int)exception.StatusCode,
                    address: exception.Address,
                    traceId: exception.TraceId
                );

                ErrorRequestHandler.HandleInternalError(context);
                break;


            case StorageServiceUnavailableException exception:

                _logger.LogStorageServiceUnavailable(
                    callId: callId,
                    curTime: DateTime.UtcNow,
                    address: exception.Address
                );

                ErrorRequestHandler.HandleInternalError(context);
                break;

            case WordCloudApiResponseException exception:

                _logger.LogWordCloudApiUnexpectedResponse(
                    callId: callId,
                    curTime: DateTime.UtcNow,
                    address: exception.Address,
                    statusCode: exception.StatusCode,
                    statusCodeInt: (int)exception.StatusCode
                );

                ErrorRequestHandler.HandleInternalError(context);
                break;

            case WordCloutApiUnavailableException exception:

                _logger.LogWordCloudApuUnavailable(
                    callId: callId,
                    curTime: DateTime.UtcNow,
                    address: exception.Address
                );

                ErrorRequestHandler.HandleInternalError(context);
                break;

            default:
                _logger.LogInternalError(
                    callId: callId,
                    curTime: DateTime.UtcNow,
                    exception: context.Exception
                );

                ErrorRequestHandler.HandleInternalError(context);
                break;
        }
    }
}