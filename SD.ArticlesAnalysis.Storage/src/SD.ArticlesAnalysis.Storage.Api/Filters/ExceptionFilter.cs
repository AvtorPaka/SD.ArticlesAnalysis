using Microsoft.AspNetCore.Mvc.Filters;
using SD.ArticlesAnalysis.Storage.Api.Extensions;
using SD.ArticlesAnalysis.Storage.Api.Filters.Utils;
using SD.ArticlesAnalysis.Storage.Domain.Exceptions.Domain;
using SD.ArticlesAnalysis.Storage.Domain.Exceptions.Domain.Articles;

namespace SD.ArticlesAnalysis.Storage.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        string callId = context.HttpContext.TraceIdentifier;

        switch (context.Exception)
        {
            case BadRequestException exception:
                _logger.LogDomainBadRequestError(
                    callId: callId,
                    curTime: DateTime.UtcNow,
                    violations: exception.ToString()
                );
                
                ErrorRequestHandler.HandleBadRequestError(context, exception);
                break;

            case ArticleDataNotFoundException exception:
                _logger.LogArticleDataNotFound(
                    callId: callId,
                    curTime: DateTime.UtcNow,
                    articleId: exception.InvalidId,
                    invalidPath: exception.InvalidPath
                );

                ErrorRequestHandler.HandleArticleNotFoundError(context, exception);
                break;

            case ArticleNotFoundException exception:
                _logger.LogArticleMetaNotFound(
                    callId: callId,
                    curTime: DateTime.UtcNow,
                    articleId: exception.InvalidId
                );

                ErrorRequestHandler.HandleArticleNotFoundError(context, exception);
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