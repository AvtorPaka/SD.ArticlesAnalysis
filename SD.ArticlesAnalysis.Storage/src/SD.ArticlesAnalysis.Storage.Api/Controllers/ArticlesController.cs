using Microsoft.AspNetCore.Mvc;
using SD.ArticlesAnalysis.Storage.Api.Contracts.Requests;
using SD.ArticlesAnalysis.Storage.Api.Contracts.Responses;
using SD.ArticlesAnalysis.Storage.Api.Filters;
using SD.ArticlesAnalysis.Storage.Domain.Models;
using SD.ArticlesAnalysis.Storage.Domain.Services.Interfaces;

namespace SD.ArticlesAnalysis.Storage.Api.Controllers;

[ApiController]
[Route("/articles")]
public class ArticlesController : ControllerBase
{
    private readonly IArticlesTransferService _articlesTransferService;

    public ArticlesController(IArticlesTransferService articlesTransferService)
    {
        _articlesTransferService = articlesTransferService;
    }

    [HttpPost]
    [Route("upload")]
    [ProducesResponseType<UploadArticleResponse>(200)]
    [ErrorResponse(400)]
    public async Task<IActionResult> UploadArticle([FromForm] IFormFile article, CancellationToken cancellationToken)
    {
        await using var articleDataStream = article.OpenReadStream();

        ArticleMetaModel articleModel = await _articlesTransferService.UploadArticle(
            articleDataStream: articleDataStream,
            articleName: article.FileName,
            cancellationToken: cancellationToken
        );

        return Ok(new UploadArticleResponse(
            Id: articleModel.Id
        ));
    }

    [HttpGet]
    [Route("get")]
    [ErrorResponse(404)]
    public async Task<IActionResult> GetArticle([FromQuery] GetArticleRequest request,
        CancellationToken cancellationToken)
    {
        var downloadContainer = await _articlesTransferService.DownloadArticle(
            id: request.Id,
            cancellationToken: cancellationToken
        );

        if (request.Download)
        {
            return File(
                fileStream: downloadContainer.ArticleFileStream,
                contentType: "application/octet-stream",
                fileDownloadName: downloadContainer.DispositionFilename
            );
        }
        
        return File(
            fileStream: downloadContainer.ArticleFileStream,
            contentType: "application/octet-stream"
        );
    }
}