using Microsoft.Extensions.Options;
using SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Interfaces;
using SD.ArticlesAnalysis.Storage.Domain.Exceptions.Infrastructure;
using SD.ArticlesAnalysis.Storage.Infrastructure.Configuration.Options;

namespace SD.ArticlesAnalysis.Storage.Infrastructure.Dal.Repositories;

public class ArticlesDataRepository : IArticlesDataRepository
{
    private readonly FileStorageLocationOptions _storageLocationOptions;

    public ArticlesDataRepository(IOptions<FileStorageLocationOptions> locationOptions)
    {
        _storageLocationOptions = locationOptions.Value;
    }

    public async Task<string> UploadArticleFile(string articleUniqueName, Stream articleDataStream,
        CancellationToken cancellationToken)
    {
        var articleUniqueLocation = Path.Combine(_storageLocationOptions.ArticlesDataLocation, articleUniqueName);

        await using var fs = new FileStream(
            path: articleUniqueLocation,
            mode: FileMode.Create,
            access: FileAccess.Write
        );
        await articleDataStream.CopyToAsync(fs, cancellationToken);

        return articleUniqueLocation;
    }


    public async Task<Stream> GetArticleFileStream(string articlePath, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); // Some fiction
        if (!File.Exists(articlePath))
        {
            throw new DataFileNotFoundException(
                message: "",
                fileLocation: articlePath
            );
        }

        return new FileStream(
            path: articlePath,
            mode: FileMode.Open,
            access: FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096,
            options: FileOptions.Asynchronous
        );
    }
}