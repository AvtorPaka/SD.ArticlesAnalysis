namespace SD.ArticlesAnalysis.Analysis.Domain.Models;

public record ArticleAnalysisModel(
    long Id,
    long ArticleId,
    string ArticleName,
    long ParagraphsCount,
    long WordsCount,
    long CharactersCount,
    string WordCloudLocation
);