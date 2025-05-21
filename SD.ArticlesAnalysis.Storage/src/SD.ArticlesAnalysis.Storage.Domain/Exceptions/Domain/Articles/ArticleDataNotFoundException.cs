using SD.ArticlesAnalysis.Storage.Domain.Exceptions.Infrastructure;

namespace SD.ArticlesAnalysis.Storage.Domain.Exceptions.Domain.Articles;

public class ArticleDataNotFoundException: DomainException
{
    public long InvalidId { get; init; }
    public string? InvalidPath { get; init; }
    
    public ArticleDataNotFoundException(string? message, long invalidId, string? invalidPath, DataFileNotFoundException? innerException) : base(message, innerException)
    {
        InvalidId = invalidId;
        InvalidPath = invalidPath;
    }
    
}