using SD.ArticlesAnalysis.Storage.Domain.Containers;
using SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Interfaces;
using SD.ArticlesAnalysis.Storage.Domain.Models;
using SD.ArticlesAnalysis.Storage.Domain.Services.Interfaces;

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
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    public async Task<DownloadArticleContainer> DownloadArticle(long id, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }
}