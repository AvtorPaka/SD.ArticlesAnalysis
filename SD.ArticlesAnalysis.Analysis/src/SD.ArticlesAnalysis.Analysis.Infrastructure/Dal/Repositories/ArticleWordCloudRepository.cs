using Microsoft.Extensions.Options;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Interfaces;
using SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Dal;
using SD.ArticlesAnalysis.Analysis.Infrastructure.Configuration.Options;

namespace SD.ArticlesAnalysis.Analysis.Infrastructure.Dal.Repositories;

public class ArticleWordCloudRepository : IArticleWordCloudRepository
{
    private readonly FileStorageLocationOptions _locationOptions;

    public ArticleWordCloudRepository(IOptions<FileStorageLocationOptions> fileStorageLocationOptions)
    {
        _locationOptions = fileStorageLocationOptions.Value;
    }

    public async Task<string> UploadWordCloudImage(Stream wordCloudImageStream, string wordCloudUniqueName,
        CancellationToken cancellationToken)
    {
        var wordCloudUniqueLocation = Path.Combine(_locationOptions.WordCloudDataLocation, wordCloudUniqueName);

        await using var fs = new FileStream(
            path: wordCloudUniqueLocation,
            mode: FileMode.Create,
            access: FileAccess.Write
        );

        long initialPosition = wordCloudImageStream.Position;
        if (wordCloudImageStream.CanSeek)
        {
            wordCloudImageStream.Position = 0;
        }

        await wordCloudImageStream.CopyToAsync(fs, cancellationToken);

        wordCloudImageStream.Position = initialPosition;

        return wordCloudUniqueLocation;
    }

    public async Task<Stream> GetWordCloudImageFileStream(string wordCloudImagePath,
        CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); // Some fiction

        if (!File.Exists(wordCloudImagePath))
        {
            throw new DataFileNotFoundException(
                message: "Word cloud image could not be found.",
                fileLocation: wordCloudImagePath
            );
        }

        return new FileStream(
            path: wordCloudImagePath,
            mode: FileMode.Open,
            access: FileAccess.Read,
            share: FileShare.Read,
            bufferSize: 4096,
            options: FileOptions.Asynchronous
        );
    }
}