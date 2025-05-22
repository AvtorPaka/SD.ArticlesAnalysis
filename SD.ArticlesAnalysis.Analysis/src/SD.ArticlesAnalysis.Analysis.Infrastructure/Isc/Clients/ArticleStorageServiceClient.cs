using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Dto.Responses;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Interfaces;
using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Isc.Storage;

namespace SD.ArticlesAnalysis.Analysis.Infrastructure.Isc.Clients;

public class ArticleStorageServiceClient : IArticleStorageServiceClient
{
    internal const string ArticleStorageClientTag = "SD.AA.StorageClient";
    private readonly IHttpClientFactory _httpClientFactory;

    public ArticleStorageServiceClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<GetArticleDataResponse> GetArticleData(long articleId, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient(ArticleStorageClientTag);

        HttpResponseMessage response;

        try
        {
            response = await client.GetAsync(
                requestUri: $"/articles/get?Id={articleId}",
                cancellationToken: cancellationToken
            );
        }
        catch (HttpRequestException ex)
        {
            throw new StorageServiceUnavailableException(
                message: "Unable to get the response from SD.AA.Storage service",
                address: client.BaseAddress,
                innerException: ex
            );
        }

        if (!response.IsSuccessStatusCode)
        {
            string traceId = string.Empty;
            if (response.Headers.TryGetValues("X-Trace-Id", out var traceHeaders))
            {
                traceId = traceHeaders.FirstOrDefault() ?? "";
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new StorageServiceArticleNotFoundException(
                    message: "Article data not found on SD.AA.Storage service",
                    articleId: articleId,
                    traceId: traceId
                );
            }

            throw new StorageServiceResponseException(
                message: "Unexpected response from SD.AA.Storage service",
                statusCode: response.StatusCode,
                address: client.BaseAddress,
                traceId: traceId
            );
        }

        string articleName = GetArticleName(response.Content.Headers) ?? $"{articleId}";
        string articleData = await GetArticleData(response.Content);

        return new GetArticleDataResponse(
            ArticleData: articleData,
            ArticleName: articleName
        );
    }

    private async Task<string> GetArticleData(HttpContent responseContent)
    {
        byte[] responseBytes = await responseContent.ReadAsByteArrayAsync();
        return Encoding.UTF8.GetString(responseBytes);
    }

    private string? GetArticleName(HttpContentHeaders headers)
    {
        if (headers.TryGetValues("Content-Disposition", out var dispositionHeaders))
        {
            var dispositionHeader = dispositionHeaders.FirstOrDefault();

            if (!string.IsNullOrEmpty(dispositionHeader))
            {
                var contentDisposition = new ContentDisposition(dispositionHeader);
                return contentDisposition.FileName?.Split(".", StringSplitOptions.RemoveEmptyEntries)[^2];
            }

            return null;
        }

        return null;
    }
}