using Microsoft.AspNetCore.Mvc;
using SD.ArticlesAnalysis.Analysis.Api.Contracts.Requests;
using SD.ArticlesAnalysis.Analysis.Api.Contracts.Responses;
using SD.ArticlesAnalysis.Analysis.Api.Filters;
using SD.ArticlesAnalysis.Analysis.Domain.Containers;
using SD.ArticlesAnalysis.Analysis.Domain.Models;
using SD.ArticlesAnalysis.Analysis.Domain.Services.Interfaces;

namespace SD.ArticlesAnalysis.Analysis.Api.Controllers;

[ApiController]
[Route("articles")]
public class ArticlesAnalysisController : ControllerBase
{
    private readonly IArticleAnalysisService _articleAnalysisService;

    public ArticlesAnalysisController(IArticleAnalysisService analysisService)
    {
        _articleAnalysisService = analysisService;
    }

    [HttpGet]
    [Route("get")]
    [ProducesResponseType<GetArticleAnalysisResponse>(200)]
    [ErrorResponse(404)]
    public async Task<IActionResult> GetAnalysis([FromQuery] GetArticleAnalysisRequest request,
        CancellationToken cancellationToken)
    {
        ArticleAnalysisModel model = await _articleAnalysisService.AnalyzeArticle(
            articleId: request.Id,
            cancellationToken: cancellationToken
        );

        return Ok(new GetArticleAnalysisResponse(
            ArticleId: model.ArticleId,
            ArticleName: model.ArticleName,
            Paragraphs: model.ParagraphsCount,
            Words: model.WordsCount,
            Characters: model.CharactersCount
        ));
    }

    [HttpGet]
    [Route("word-cloud")]
    [ErrorResponse(400)]
    [ErrorResponse(404)]
    public async Task<IActionResult> GetWordCloud([FromQuery] GetArticleWordCloudRequest request,
        CancellationToken cancellationToken)
    {
        DownloadArticleWordCloudContainer container = await _articleAnalysisService.DownloadWordCloudImage(
            articleId: request.Id,
            cancellationToken: cancellationToken
        );

        Response.Headers.CacheControl = "public,max-age=3600";
        return File(container.WordCloudFileStream, "image/png", container.DispositionFilename);
    }
}