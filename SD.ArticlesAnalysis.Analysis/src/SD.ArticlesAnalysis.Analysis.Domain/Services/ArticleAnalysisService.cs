using SD.ArticlesAnalysis.Analysis.Domain.Containers;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Interfaces;
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
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    public async Task<DownloadArticleWordCloudContainer> DownloadWordCloudImage(long articleId, CancellationToken cancellationToken)
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

    private async Task<DownloadArticleWordCloudContainer> DownloadWordCloudImageUnsafe(long articleId, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken);
        throw new NotImplementedException();
    }
}