using Npgsql;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Entities;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Interfaces;

namespace SD.ArticlesAnalysis.Analysis.Infrastructure.Dal.Repositories;

public class ArticleAnalysisRepository: BaseRepository, IArticleAnalysisRepository
{
    public ArticleAnalysisRepository(NpgsqlDataSource npgsqlDataSource) : base(npgsqlDataSource)
    {
    }
    
    public async Task<ArticleAnalysisEntity> GetByArticleId(long articleId, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    public async Task<long> Add(ArticleAnalysisEntity entity, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken);
        throw new NotImplementedException();
    }
}