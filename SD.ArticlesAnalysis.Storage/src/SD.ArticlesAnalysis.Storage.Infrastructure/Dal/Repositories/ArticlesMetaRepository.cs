using Npgsql;
using SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Entities;
using SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Interfaces;

namespace SD.ArticlesAnalysis.Storage.Infrastructure.Dal.Repositories;

public class ArticlesMetaRepository: BaseRepository, IArticlesMetaRepository
{
    public ArticlesMetaRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
    }

    public async Task<ArticleMetaEntity> GetById(long id, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    public async Task<ArticleMetaEntity?> GetByHash(string hash, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    public async Task<long> Add(ArticleMetaEntity entity, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }
}