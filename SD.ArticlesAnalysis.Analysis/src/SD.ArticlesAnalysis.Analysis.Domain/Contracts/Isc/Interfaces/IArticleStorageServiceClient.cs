using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Dto.Responses;

namespace SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Interfaces;

public interface IArticleStorageServiceClient
{
    public Task<GetArticleDataResponse> GetArticleData(long articleId, CancellationToken cancellationToken);
}