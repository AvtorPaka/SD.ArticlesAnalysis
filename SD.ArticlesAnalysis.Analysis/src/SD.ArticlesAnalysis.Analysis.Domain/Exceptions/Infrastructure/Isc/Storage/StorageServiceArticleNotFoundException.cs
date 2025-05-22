namespace SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Isc.Storage;

public class StorageServiceArticleNotFoundException: InfrastructureException
{
    public long ArticleId { get; init; }
    public string? TraceId { get; init; }
    
    public StorageServiceArticleNotFoundException(string? message, long articleId, string? traceId) : base(message)
    {
        ArticleId = articleId;
        TraceId = traceId;
    }
}