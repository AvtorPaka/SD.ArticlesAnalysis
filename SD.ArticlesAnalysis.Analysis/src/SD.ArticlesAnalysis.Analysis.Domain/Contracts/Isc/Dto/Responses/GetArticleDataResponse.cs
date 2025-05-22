namespace SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Dto.Responses;

public record GetArticleDataResponse(
    string ArticleData,
    string ArticleName
);