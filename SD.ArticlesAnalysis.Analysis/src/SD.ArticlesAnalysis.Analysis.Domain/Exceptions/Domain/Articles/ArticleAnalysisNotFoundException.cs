
using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Dal;

namespace SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Domain.Articles;

public class ArticleAnalysisNotFoundException : DomainException
{
    public long ArticleId { get; init; }

    public ArticleAnalysisNotFoundException(string? message, long articleId, EntityNotFoundException? innerException) : base(message, innerException)
    {
        ArticleId = articleId;
    }
}