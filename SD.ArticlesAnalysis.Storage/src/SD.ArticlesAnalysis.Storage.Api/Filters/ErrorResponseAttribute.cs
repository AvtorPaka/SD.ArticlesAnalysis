using Microsoft.AspNetCore.Mvc;
using SD.ArticlesAnalysis.Storage.Api.Contracts.Responses;

namespace SD.ArticlesAnalysis.Storage.Api.Filters;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ErrorResponseAttribute: ProducesResponseTypeAttribute
{
    public ErrorResponseAttribute(int statusCode) : base(typeof(ErrorResponse), statusCode)
    {
    }
}