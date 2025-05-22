using System.Text.RegularExpressions;
using SD.ArticlesAnalysis.Analysis.Domain.Containers;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Entities;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Interfaces;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Dto.Request;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Dto.Responses;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Interfaces;
using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Domain.Articles;
using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Dal;
using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Isc.Storage;
using SD.ArticlesAnalysis.Analysis.Domain.Models;
using SD.ArticlesAnalysis.Analysis.Domain.Services.Interfaces;

namespace SD.ArticlesAnalysis.Analysis.Domain.Services;

public class ArticleAnalysisService : IArticleAnalysisService
{
    private readonly IArticleAnalysisRepository _analysisRepository;
    private readonly IArticleWordCloudRepository _wordCloudRepository;
    private readonly IArticleStorageServiceClient _storageServiceClient;
    private readonly IWordCloudApiClient _wordCloudApiClient;

    public ArticleAnalysisService(
        IArticleAnalysisRepository analysisRepository,
        IArticleWordCloudRepository wordCloudRepository,
        IArticleStorageServiceClient articleStorageServiceClient,
        IWordCloudApiClient wordCloudApiClient
    )
    {
        _analysisRepository = analysisRepository;
        _wordCloudRepository = wordCloudRepository;
        _storageServiceClient = articleStorageServiceClient;
        _wordCloudApiClient = wordCloudApiClient;
    }

    public async Task<ArticleAnalysisModel> AnalyzeArticle(long articleId, CancellationToken cancellationToken)
    {
        try
        {
            return await AnalyzeArticleUnsafe(articleId, cancellationToken);
        }
        catch (StorageServiceArticleNotFoundException ex)
        {
            throw new ArticleNotFoundException(
                message: $"Article with id: {articleId} not found on SD.AA.Storage serivce",
                invalidArticleId: articleId,
                innerException: ex
            );
        }
    }

    private async Task<ArticleAnalysisModel> AnalyzeArticleUnsafe(long articleId, CancellationToken cancellationToken)
    {
        using var transaction = _analysisRepository.CreateTransactionScope();

        ArticleAnalysisEntity? existingAnalysis = await _analysisRepository.GetByArticleId(
            articleId: articleId,
            cancellationToken: cancellationToken
        );

        ArticleAnalysisModel analysisModel;
        if (existingAnalysis != null)
        {
            analysisModel = new ArticleAnalysisModel(
                Id: existingAnalysis.Id,
                ArticleId: existingAnalysis.ArticleId,
                ArticleName: existingAnalysis.ArticleName,
                TextAnalysis: new ArticleTextAnalysisModel(
                    ParagraphsCount: existingAnalysis.ParagraphsCount,
                    WordsCount: existingAnalysis.WordsCount,
                    CharactersCount: existingAnalysis.CharactersCount
                ),
                WordCloudLocation: existingAnalysis.WordCloudLocation
            );
        }
        else
        {
            GetArticleDataResponse articleData = await _storageServiceClient.GetArticleData(
                articleId: articleId,
                cancellationToken: cancellationToken
            );

            string wordCloudUniqueName = $"{articleId}_wc_{articleData.ArticleName}.png";

            await using Stream wordCloudImageStream = await _wordCloudApiClient.GetArticleWordCloudImage(
                request: new GetArticleWordCloudRequest(
                    Text: articleData.ArticleData,
                    Format: "png",
                    Width: 600,
                    Height: 600,
                    FontFamily: "sans-serif",
                    FontScale: 20
                ),
                cancellationToken: cancellationToken
            );

            string articleWordCloudLocationPath = await _wordCloudRepository.UploadWordCloudImage(
                wordCloudImageStream: wordCloudImageStream,
                wordCloudUniqueName: wordCloudUniqueName,
                cancellationToken: cancellationToken
            );

            ArticleTextAnalysisModel textAnalysis = ProcessArticleText(articleData.ArticleData);

            var createdIds = await _analysisRepository.Add(
                entities:
                [
                    new ArticleAnalysisEntity
                    {
                        ArticleId = articleId,
                        ArticleName = articleData.ArticleName,
                        CharactersCount = textAnalysis.CharactersCount,
                        ParagraphsCount = textAnalysis.ParagraphsCount,
                        WordsCount = textAnalysis.WordsCount,
                        WordCloudLocation = articleWordCloudLocationPath
                    }
                ],
                cancellationToken: cancellationToken
            );

            long createdId = createdIds.Length == 0 ? -1 : createdIds[0];

            analysisModel = new ArticleAnalysisModel(
                Id: createdId,
                ArticleId: articleId,
                ArticleName: articleData.ArticleName,
                WordCloudLocation: articleWordCloudLocationPath,
                TextAnalysis: textAnalysis
            );
        }


        transaction.Complete();
        return analysisModel;
    }

    private ArticleTextAnalysisModel ProcessArticleText(string articleText)
    {
        long paragraphs = articleText.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries)
            .Count(p => !string.IsNullOrWhiteSpace(p));
        
        long words = Regex.Split(articleText, @"\W+")
            .Count(w => !string.IsNullOrWhiteSpace(w));;

        long characters = articleText.Count(c => !char.IsWhiteSpace(c));

        return new ArticleTextAnalysisModel(
            ParagraphsCount: paragraphs,
            WordsCount: words,
            CharactersCount: characters
        );
    }

    public async Task<DownloadArticleWordCloudContainer> DownloadWordCloudImage(long articleId,
        CancellationToken cancellationToken)
    {
        try
        {
            return await DownloadWordCloudImageUnsafe(articleId, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            throw new ArticleAnalysisNotFoundException(
                message: $"Analysis for article with id: {articleId} not found",
                articleId: articleId,
                innerException: ex
            );
        }
        catch (DataFileNotFoundException ex)
        {
            throw new ArticleWordCloudNotFoundException(
                message: $"Word cloud image for article with id: {articleId} not found for path: {ex.FileLocation}",
                articleId: articleId,
                invalidPath: ex.FileLocation,
                innerException: ex
            );
        }
    }

    private async Task<DownloadArticleWordCloudContainer> DownloadWordCloudImageUnsafe(long articleId,
        CancellationToken cancellationToken)
    {
        using var transaction = _analysisRepository.CreateTransactionScope();

        ArticleAnalysisEntity? entity = await _analysisRepository.GetByArticleId(
            articleId: articleId,
            cancellationToken: cancellationToken
        );

        if (entity == null)
        {
            throw new EntityNotFoundException("Article analysis could not be found");
        }

        transaction.Complete();

        Stream wordCloudImageStream = await _wordCloudRepository.GetWordCloudImageFileStream(
            wordCloudImagePath: entity.WordCloudLocation,
            cancellationToken: cancellationToken
        );

        return new DownloadArticleWordCloudContainer(
            WordCloudFileStream: wordCloudImageStream,
            DispositionFilename: $"{entity.ArticleName}_word_cloud.png"
        );
    }
}