using SD.ArticlesAnalysis.Storage.Domain.Containers;
using SD.ArticlesAnalysis.Storage.Domain.Models;

namespace SD.ArticlesAnalysis.Storage.Domain.Services.Interfaces;

public interface IArticlesTransferService
{
    public Task<ArticleMetaModel> UploadArticle(Stream articleDataStream, string articleName,
        CancellationToken cancellationToken);

    public Task<DownloadArticleContainer> DownloadArticle(long id, CancellationToken cancellationToken);
}