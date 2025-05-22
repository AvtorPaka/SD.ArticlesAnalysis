using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SD.ArticlesAnalysis.Analysis.Api.Contracts.Responses;
using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Domain.Articles;

namespace SD.ArticlesAnalysis.Analysis.Api.Filters.Utils;

internal static class ErrorRequestHandler
{
    
    internal static void HandleArticleNotFoundError(ExceptionContext context, ArticleNotFoundException exception)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.NotFound,
                Message: $"Article with id: {exception.InvalidArticleId} not found."
                )
            )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.NotFound
        };

        context.Result = result;
    }
    
    internal static void HandleArticleWordCloudNotFoundError(ExceptionContext context, ArticleWordCloudNotFoundException exception)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.NotFound,
                Message: $"Word cloud for article with id: {exception.ArticleId} not found."
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.NotFound
        };

        context.Result = result;
    }
    
    internal static void HandleArticleAnalysisBadRequestError(ExceptionContext context, ArticleAnalysisNotFoundException exception)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.BadRequest,
                Message: $"Analysis for article with id: {exception.ArticleId} not present."
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.BadRequest
        };

        context.Result = result;
    }

    
    internal static void HandleInternalError(ExceptionContext context)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.InternalServerError,
                Message: "Internal error. Check SD.AA.Analysis logs for detailed description"
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        context.Result = result;
    }
}