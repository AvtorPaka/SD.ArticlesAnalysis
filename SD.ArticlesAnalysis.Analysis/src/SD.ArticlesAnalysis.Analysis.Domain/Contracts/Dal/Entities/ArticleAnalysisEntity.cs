namespace SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Entities;

public class ArticleAnalysisEntity
{
    public long Id { get; init; }
    public long ArticleId { get; init; }
    public string ArticleName { get; init; } = string.Empty;
    public long ParagraphsCount { get; init; }
    public long WordsCount { get; init; }
    public long CharactersCount { get; init; }
    public string WordCloudLocation { get; init; } = string.Empty;
}