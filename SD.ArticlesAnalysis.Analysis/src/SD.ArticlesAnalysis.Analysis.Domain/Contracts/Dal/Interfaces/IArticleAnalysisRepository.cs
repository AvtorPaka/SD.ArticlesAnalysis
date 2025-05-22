using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Entities;

namespace SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Interfaces;

public interface IArticleAnalysisRepository: IDbRepository
{
    public Task<ArticleAnalysisEntity> GetByArticleId(long articleId, CancellationToken cancellationToken);
    public Task<long[]> Add(ArticleAnalysisEntity[] entities, CancellationToken cancellationToken);
}