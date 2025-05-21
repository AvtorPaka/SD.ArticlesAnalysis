namespace SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Interfaces;

public interface IArticlesDataRepository
{
    public Task<string> UploadArticleFile(string articleUniqueName, Stream articleDataStream, CancellationToken cancellationToken);
    public Task<Stream> GetArticleFileStream(string articlePath, CancellationToken cancellationToken);
}