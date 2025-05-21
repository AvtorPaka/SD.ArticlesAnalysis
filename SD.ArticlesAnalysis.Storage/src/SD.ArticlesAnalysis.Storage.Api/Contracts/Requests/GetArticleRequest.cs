namespace SD.ArticlesAnalysis.Storage.Api.Contracts.Requests;

public record GetArticleRequest(
    long Id,
    bool Download
);