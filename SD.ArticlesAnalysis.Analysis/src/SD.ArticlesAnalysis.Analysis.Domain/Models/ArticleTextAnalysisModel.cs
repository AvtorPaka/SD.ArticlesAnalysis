namespace SD.ArticlesAnalysis.Analysis.Domain.Models;

public record ArticleTextAnalysisModel(
    long ParagraphsCount,
    long WordsCount,
    long CharactersCount
);