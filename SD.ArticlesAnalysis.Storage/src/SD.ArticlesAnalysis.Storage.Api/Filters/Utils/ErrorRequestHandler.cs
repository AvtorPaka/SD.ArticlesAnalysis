using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SD.ArticlesAnalysis.Storage.Api.Contracts.Responses;
using SD.ArticlesAnalysis.Storage.Domain.Exceptions.Domain;
using SD.ArticlesAnalysis.Storage.Domain.Exceptions.Domain.Articles;

namespace SD.ArticlesAnalysis.Storage.Api.Filters.Utils;

internal static class ErrorRequestHandler
{
    internal static void HandleBadRequestError(ExceptionContext context, BadRequestException exception)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.BadRequest,
                Message: exception.ToString()
                )
            )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.BadRequest
        };

        context.Result = result;
    }
    
    
    internal static void HandleArticleNotFoundError(ExceptionContext context, ArticleNotFoundException exception)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.NotFound,
                Message: $"Article with id: {exception.InvalidId} not found"
                )
            )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.NotFound
        };

        context.Result = result;
    }
    
    internal static void HandleArticleNotFoundError(ExceptionContext context, ArticleDataNotFoundException exception)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.NotFound,
                Message: $"Article with id: {exception.InvalidId} not found"
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.NotFound
        };

        context.Result = result;
    }
    
    
    internal static void HandleInternalError(ExceptionContext context)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.InternalServerError,
                Message: "Internal error. Check SD.AA.Storage logs for detailed description"
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        context.Result = result;
    }
}