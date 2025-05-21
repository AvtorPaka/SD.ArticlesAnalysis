namespace SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Entities;

public class ArticleMetaEntity
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Hash { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
}