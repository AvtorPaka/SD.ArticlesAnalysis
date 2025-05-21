namespace SD.ArticlesAnalysis.Storage.Domain.Models;

public record ArticleMetaModel(
    long Id,
    string Name,
    string Hash,
    string Location
);