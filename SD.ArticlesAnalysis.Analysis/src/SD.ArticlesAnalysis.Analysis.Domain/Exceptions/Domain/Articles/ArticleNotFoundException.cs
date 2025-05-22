using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Isc.Storage;

namespace SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Domain.Articles;

public class ArticleNotFoundException : DomainException
{
    public long InvalidArticleId { get; init; }
    public StorageServiceArticleNotFoundException? IscException { get; init; }

    public ArticleNotFoundException(string? message, long invalidArticleId,
        StorageServiceArticleNotFoundException? innerException) : base(message, innerException)
    {
        InvalidArticleId = invalidArticleId;
        IscException = innerException;
    }
}