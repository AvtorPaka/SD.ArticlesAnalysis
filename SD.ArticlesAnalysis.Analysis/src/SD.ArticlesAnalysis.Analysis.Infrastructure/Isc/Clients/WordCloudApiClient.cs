using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Dto.Request;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Interfaces;

namespace SD.ArticlesAnalysis.Analysis.Infrastructure.Isc.Clients;

public class WordCloudApiClient: IWordCloudApiClient
{
    internal const string WordCloudApiClientTag = "WordCloudApi";
    private readonly IHttpClientFactory _httpClientFactory;

    public WordCloudApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<Stream> GetArticleWordCloudImage(GetArticleWordCloudRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken);
        throw new NotImplementedException();
    }
}