namespace SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Interfaces;

public interface IArticleWordCloudRepository
{
    public Task<string> UploadWordCloudImage(Stream wordCloudImageStream, string wordCloudUniqueName, CancellationToken cancellationToken);
    public Task<Stream> GetWordCloudImageFileStream(string wordCloudImagePath, CancellationToken cancellationToken);
}