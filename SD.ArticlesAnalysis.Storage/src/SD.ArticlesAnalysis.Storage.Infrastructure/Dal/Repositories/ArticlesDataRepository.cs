using Microsoft.Extensions.Options;
using SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Interfaces;
using SD.ArticlesAnalysis.Storage.Infrastructure.Configuration.Options;

namespace SD.ArticlesAnalysis.Storage.Infrastructure.Dal.Repositories;

public class ArticlesDataRepository: IArticlesDataRepository
{
    private readonly FileStorageLocationOptions _storageLocationOptions;

    public ArticlesDataRepository(IOptions<FileStorageLocationOptions> locationOptions)
    {
        _storageLocationOptions = locationOptions.Value;
    }
    
    public async Task<string> UploadArticleFile(string articleUniqueName, Stream articleDataStream, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    public async Task<Stream> GetArticleFileStream(string articlePath, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }
}