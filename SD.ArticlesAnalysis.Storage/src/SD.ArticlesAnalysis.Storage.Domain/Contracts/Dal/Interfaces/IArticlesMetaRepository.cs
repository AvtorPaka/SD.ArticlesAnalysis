using SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Entities;

namespace SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Interfaces;

public interface IArticlesMetaRepository: IDbRepository
{
    public Task<ArticleMetaEntity> GetById(long id, CancellationToken cancellationToken);
    public Task<ArticleMetaEntity?> GetByHash(string hash, CancellationToken cancellationToken);
    public Task<long> Add(ArticleMetaEntity entity, CancellationToken cancellationToken);
}