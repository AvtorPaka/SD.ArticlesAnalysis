using Microsoft.AspNetCore.Mvc;
using SD.ArticlesAnalysis.Analysis.Api.Contracts.Requests;
using SD.ArticlesAnalysis.Analysis.Api.Contracts.Responses;
using SD.ArticlesAnalysis.Analysis.Api.Filters;

namespace SD.ArticlesAnalysis.Analysis.Api.Controllers;

[ApiController]
[Route("articles")]
public class ArticlesAnalysisController: ControllerBase
{
    [HttpGet]
    [Route("get")]
    [ProducesResponseType<GetArticleAnalysisResponse>(200)]
    [ErrorResponse(404)]
    public async Task<IActionResult> GetAnalysis([FromQuery] GetArticleAnalysisRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("word-cloud")]
    [ErrorResponse(400)]
    [ErrorResponse(404)]
    public async Task<IActionResult> GetWordCloud([FromQuery] GetArticleWordCloudRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken);
        throw new NotImplementedException();
    }
}