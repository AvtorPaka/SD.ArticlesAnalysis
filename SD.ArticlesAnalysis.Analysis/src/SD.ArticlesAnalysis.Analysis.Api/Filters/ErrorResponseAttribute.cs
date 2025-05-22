using Microsoft.AspNetCore.Mvc;
using SD.ArticlesAnalysis.Analysis.Api.Contracts.Responses;

namespace SD.ArticlesAnalysis.Analysis.Api.Filters;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ErrorResponseAttribute: ProducesResponseTypeAttribute
{
    public ErrorResponseAttribute(int statusCode) : base(typeof(ErrorResponse), statusCode)
    {
    }
}