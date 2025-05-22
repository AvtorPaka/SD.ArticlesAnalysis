using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Dto.Request;

namespace SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Interfaces;

public interface IWordCloudApiClient
{
    public Task<Stream> GetArticleWordCloudImage(GetArticleWordCloudRequest request, CancellationToken cancellationToken);
}