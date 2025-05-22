using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Dal;

namespace SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Domain.Articles;

public class ArticleWordCloudNotFoundException : DomainException
{
    public long ArticleId { get; init; }
    public string? InvalidPath { get; init; }

    protected ArticleWordCloudNotFoundException(string? message, long articleId, string? invalidPath,  DataFileNotFoundException? innerException) : base(
        message, innerException)
    {
        ArticleId = articleId;
        InvalidPath = invalidPath;
    }
}