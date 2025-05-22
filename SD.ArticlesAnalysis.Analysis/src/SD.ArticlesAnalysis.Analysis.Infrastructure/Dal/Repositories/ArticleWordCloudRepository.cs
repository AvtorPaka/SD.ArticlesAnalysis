using Microsoft.Extensions.Options;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Interfaces;
using SD.ArticlesAnalysis.Analysis.Infrastructure.Configuration.Options;

namespace SD.ArticlesAnalysis.Analysis.Infrastructure.Dal.Repositories;

public class ArticleWordCloudRepository: IArticleWordCloudRepository
{
    private readonly FileStorageLocationOptions _locationOptions;

    public ArticleWordCloudRepository(IOptions<FileStorageLocationOptions> fileStorageLocationOptions)
    {
        _locationOptions = fileStorageLocationOptions.Value;
    }
    
    public async Task<string> UploadWordCloudImage(Stream wordCloudImageStream, string wordCloudUniqueName, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    public async Task<Stream> GetWordCloudImageFileStream(string wordCloudImagePath, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken);
        throw new NotImplementedException();
    }
}