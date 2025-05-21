namespace SD.ArticlesAnalysis.Storage.Domain.Services.Interfaces;

public interface IHasherService
{
    public string ComputeFileHash(Stream fileStream);
}