namespace SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Isc.Storage;

public class StorageServiceUnavailableException : InfrastructureException
{
    public Uri? Address { get; init; }

    public StorageServiceUnavailableException(string? message, Uri? address, HttpRequestException? innerException) :
        base(message, innerException)
    {
        Address = address;
    }
}