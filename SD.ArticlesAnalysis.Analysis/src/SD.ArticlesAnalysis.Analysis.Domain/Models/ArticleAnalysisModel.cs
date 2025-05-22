namespace SD.ArticlesAnalysis.Analysis.Domain.Models;

public record ArticleAnalysisModel(
    long Id,
    long ArticleId,
    string ArticleName,
    string WordCloudLocation,
    ArticleTextAnalysisModel TextAnalysis
);