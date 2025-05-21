using SD.ArticlesAnalysis.Storage.Domain.Containers;
using SD.ArticlesAnalysis.Storage.Domain.Models;
using SD.ArticlesAnalysis.Storage.Domain.Services.Interfaces;

namespace SD.ArticlesAnalysis.Storage.Domain.Services;

public class ArticlesTransferService: IArticlesTransferService
{
    public async Task<ArticleMetaModel> UploadArticle(Stream articleDataStream, string articleName, CancellationToken cancellationToken)
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