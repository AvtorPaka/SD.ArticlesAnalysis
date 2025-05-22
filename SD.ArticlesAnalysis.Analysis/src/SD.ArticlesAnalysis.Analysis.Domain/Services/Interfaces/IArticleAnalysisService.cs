using SD.ArticlesAnalysis.Analysis.Domain.Containers;
using SD.ArticlesAnalysis.Analysis.Domain.Models;

namespace SD.ArticlesAnalysis.Analysis.Domain.Services.Interfaces;

public interface IArticleAnalysisService
{
    public Task<ArticleAnalysisModel> AnalyzeArticle(long articleId, CancellationToken cancellationToken);

    public Task<DownloadArticleWordCloudContainer> DownloadWordCloudImage(long articleId,
        CancellationToken cancellationToken);
}