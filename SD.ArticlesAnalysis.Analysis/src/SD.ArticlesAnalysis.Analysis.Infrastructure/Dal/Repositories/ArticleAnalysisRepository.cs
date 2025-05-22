using Dapper;
using Npgsql;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Entities;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Interfaces;
using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Dal;

namespace SD.ArticlesAnalysis.Analysis.Infrastructure.Dal.Repositories;

public class ArticleAnalysisRepository : BaseRepository, IArticleAnalysisRepository
{
    public ArticleAnalysisRepository(NpgsqlDataSource npgsqlDataSource) : base(npgsqlDataSource)
    {
    }
    
    public async Task<long[]> Add(ArticleAnalysisEntity[] entities, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
INSERT INTO articles_analysis (article_id, article_name, paragraphs_count, words_count, characters_count, word_cloud_location)
    SELECT article_id, article_name, paragraphs_count, words_count, characters_count, word_cloud_location
    FROM UNNEST(@Entities::article_analysis_type[])
    RETURNING id;
";

        var sqlParameters = new
        {
            Entities = entities
        };

        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var createdIds = await connection.QueryAsync<long>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlParameters,
                cancellationToken: cancellationToken
            )
        );

        return createdIds.ToArray();
    }

    public async Task<ArticleAnalysisEntity?> GetByArticleId(long articleId, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
SELECT * FROM articles_analysis
    WHERE article_id = @ArticleId;
";

        var sqlParameters = new
        {
            ArticleId = articleId
        };

        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var entities = await connection.QueryAsync<ArticleAnalysisEntity>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlParameters,
                cancellationToken: cancellationToken
            )
        );

        return  entities.FirstOrDefault();
    }
}