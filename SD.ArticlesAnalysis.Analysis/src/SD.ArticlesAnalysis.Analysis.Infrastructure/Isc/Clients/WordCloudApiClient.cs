using System.Net.Http.Json;
using System.Text.Json;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Dto.Request;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Interfaces;
using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Isc.WordCloud;

namespace SD.ArticlesAnalysis.Analysis.Infrastructure.Isc.Clients;

public class WordCloudApiClient : IWordCloudApiClient
{
    internal const string WordCloudApiClientTag = "WordCloudApi";
    private readonly IHttpClientFactory _httpClientFactory;

    public WordCloudApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Stream> GetArticleWordCloudImage(GetArticleWordCloudRequest request,
        CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient(WordCloudApiClientTag);

        HttpResponseMessage response;
        try
        {
            response = await client.PostAsJsonAsync(
                requestUri: "/wordcloud",
                value: request,
                cancellationToken: cancellationToken
            );
        }
        catch (HttpRequestException ex)
        {
            throw new WordCloutApiUnavailableException(
                message: "Unable to get the response from Word Cloud api",
                address: client.BaseAddress,
                innerException: ex
            );
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new WordCloudApiResponseException(
                message: "Unexpected response from Word Cloud api",
                statusCode: response.StatusCode,
                address: client.BaseAddress
            );
        }

        return await response.Content.ReadAsStreamAsync(cancellationToken);
    }
}