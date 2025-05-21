using Dapper;
using Npgsql;
using SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Entities;
using SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Interfaces;
using SD.ArticlesAnalysis.Storage.Domain.Exceptions.Infrastructure;

namespace SD.ArticlesAnalysis.Storage.Infrastructure.Dal.Repositories;

public class ArticlesMetaRepository : BaseRepository, IArticlesMetaRepository
{
    public ArticlesMetaRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
    }

    public async Task<ArticleMetaEntity> GetById(long id, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
SELECT * FROM articles_meta
    WHERE id = @Id;
";
        var sqlParameters = new
        {
            Id = id
        };

        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var entities = await connection.QueryAsync<ArticleMetaEntity>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlParameters,
                cancellationToken: cancellationToken
            )
        );

        var entity = entities.FirstOrDefault();
        if (entity == null)
        {
            throw new EntityNotFoundException("Article metadata could not be found.");
        }

        return entity;
    }

    public async Task<ArticleMetaEntity?> GetByHash(string hash, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
SELECT * FROM articles_meta
    WHERE hash = @ContentHash;
";
        var sqlParameters = new
        {
            ContentHash = hash
        };

        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var entities = await connection.QueryAsync<ArticleMetaEntity>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlParameters,
                cancellationToken: cancellationToken
            )
        );

        return entities.FirstOrDefault();
    }

    public async Task<long> Add(ArticleMetaEntity entity, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
INSERT INTO articles_meta (name, hash, location)
    VALUES (@Name, @ContentHash, @Location)
    RETURNING id;
";
        var sqlParameters = new
        {
            entity.Name,
            ContentHash = entity.Hash,
            entity.Location
        };

        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var ids = await connection.QueryAsync<long>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlParameters,
                cancellationToken: cancellationToken
            )
        );

        return ids.FirstOrDefault();
    }
}