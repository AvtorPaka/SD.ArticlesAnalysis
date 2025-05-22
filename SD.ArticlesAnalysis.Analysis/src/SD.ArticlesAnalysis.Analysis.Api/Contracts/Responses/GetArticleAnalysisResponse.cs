namespace SD.ArticlesAnalysis.Analysis.Api.Contracts.Responses;

public record GetArticleAnalysisResponse(
    long Id,
    long ArticleId,
    string ArticleName,
    long Paragraphs,
    long Words,
    long Characters
);