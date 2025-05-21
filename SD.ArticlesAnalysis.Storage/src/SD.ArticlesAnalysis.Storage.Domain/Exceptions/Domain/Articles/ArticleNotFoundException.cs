using SD.ArticlesAnalysis.Storage.Domain.Exceptions.Infrastructure;

namespace SD.ArticlesAnalysis.Storage.Domain.Exceptions.Domain.Articles;

public class ArticleNotFoundException: DomainException
{
    public long InvalidId { get; init; }
    
    public ArticleNotFoundException(string? message, long invalidId, EntityNotFoundException? innerException) : base(message, innerException)
    {
        InvalidId = invalidId;
    }
}