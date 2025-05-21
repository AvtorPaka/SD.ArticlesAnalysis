using FluentValidation;
using SD.ArticlesAnalysis.Storage.Domain.Containers;
using SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Entities;
using SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Interfaces;
using SD.ArticlesAnalysis.Storage.Domain.Exceptions.Domain;
using SD.ArticlesAnalysis.Storage.Domain.Exceptions.Domain.Articles;
using SD.ArticlesAnalysis.Storage.Domain.Exceptions.Infrastructure;
using SD.ArticlesAnalysis.Storage.Domain.Models;
using SD.ArticlesAnalysis.Storage.Domain.Services.Interfaces;
using SD.ArticlesAnalysis.Storage.Domain.Validators;

namespace SD.ArticlesAnalysis.Storage.Domain.Services;

public class ArticlesTransferService : IArticlesTransferService
{
    private readonly IArticlesMetaRepository _articlesMetaRepository;
    private readonly IArticlesDataRepository _articlesDataRepository;
    private readonly IHasherService _hasher;

    public ArticlesTransferService(IHasherService hasher, IArticlesMetaRepository articlesMetaRepository,
        IArticlesDataRepository articlesDataRepository)
    {
        _hasher = hasher;
        _articlesMetaRepository = articlesMetaRepository;
        _articlesDataRepository = articlesDataRepository;
    }

    public async Task<ArticleMetaModel> UploadArticle(Stream articleDataStream, string articleName,
        CancellationToken cancellationToken)
    {
        try
        {
            return await UploadArticleUnsafe(articleDataStream, articleName, cancellationToken);
        }
        catch (ValidationException ex)
        {
            throw new BadRequestException(
                message: "Invalid request parameters.",
                innerException: ex
            );
        }
    }

    private async Task<ArticleMetaModel> UploadArticleUnsafe(Stream articleDataStream, string articleName,
        CancellationToken cancellationToken)
    {
        string articleContentHash = _hasher.ComputeFileHash(articleDataStream);

        using var transaction = _articlesMetaRepository.CreateTransactionScope();

        ArticleMetaEntity? existingArticle = await _articlesMetaRepository.GetByHash(
            hash: articleContentHash,
            cancellationToken: cancellationToken
        );

        ArticleMetaModel articleMetaModel;
        if (existingArticle != null)
        {
            articleMetaModel = new ArticleMetaModel(
                Id: existingArticle.Id,
                Name: existingArticle.Name,
                Hash: existingArticle.Hash,
                Location: existingArticle.Location
            );
        }
        else
        {
            var validateArticleMetaModel = new ArticleMetaModel(
                Id: -1,
                Name: articleName,
                Hash: "",
                Location: ""
            );

            var validator = new ArticleMetaModelValidator();
            await validator.ValidateAndThrowAsync(validateArticleMetaModel, cancellationToken);

            string articleUniqueName = $"{articleContentHash}_{articleName}";

            string articleLocation = await _articlesDataRepository.UploadArticleFile(
                articleUniqueName: articleUniqueName,
                articleDataStream: articleDataStream,
                cancellationToken: cancellationToken
            );
            
            long createdMetaId = await _articlesMetaRepository.Add(
                entity: new ArticleMetaEntity
                {
                    Hash = articleContentHash,
                    Location = articleLocation,
                    Name = articleName
                },
                cancellationToken: cancellationToken
            );

            articleMetaModel = new ArticleMetaModel(
                Id: createdMetaId,
                Name: articleName,
                Hash: articleContentHash,
                Location: articleLocation
            );
        }

        transaction.Complete();
        return articleMetaModel;
    }


    public async Task<DownloadArticleContainer> DownloadArticle(long id, CancellationToken cancellationToken)
    {
        try
        {
            return await DownloadArticleUnsafe(id, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            throw new ArticleNotFoundException(
                message: $"Article with id: {id} not found.",
                invalidId: id,
                innerException: ex
            );
        }
        catch (DataFileNotFoundException ex)
        {
            throw new ArticleDataNotFoundException(
                message: $"Article data for id: {id} not found for path: {ex.FileLocation}.",
                invalidId: id,
                invalidPath: ex.FileLocation,
                innerException: ex
            );
        }
    }

    private async Task<DownloadArticleContainer> DownloadArticleUnsafe(long id, CancellationToken cancellationToken)
    {
        using var transaction = _articlesMetaRepository.CreateTransactionScope();

        ArticleMetaEntity entity = await _articlesMetaRepository.GetById(
            id: id,
            cancellationToken: cancellationToken
        );

        transaction.Complete();

        var articleFs = await _articlesDataRepository.GetArticleFileStream(
            articlePath: entity.Location,
            cancellationToken: cancellationToken
        );

        return new DownloadArticleContainer(
            ArticleFileStream: articleFs,
            DispositionFilename: entity.Name
        );
    }
}