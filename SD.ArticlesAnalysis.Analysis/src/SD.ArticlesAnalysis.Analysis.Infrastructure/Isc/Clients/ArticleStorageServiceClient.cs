using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Dto.Responses;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Interfaces;

namespace SD.ArticlesAnalysis.Analysis.Infrastructure.Isc.Clients;

public class ArticleStorageServiceClient: IArticleStorageServiceClient
{
    internal const string ArticleStorageClientTag = "SD.AA.StorageClient";
    private readonly IHttpClientFactory _httpClientFactory;

    public ArticleStorageServiceClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<GetArticleDataResponse> GetArticleData(long articleId, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken);
        throw new NotImplementedException();
    }
}