namespace SD.ArticlesAnalysis.Storage.Domain.Exceptions.Infrastructure;

public class DataFileNotFoundException: InfrastructureException
{
    public string? FileLocation { get; init; }
        
    public DataFileNotFoundException(string fileLocation)
    {
        FileLocation = fileLocation;
    }

    public DataFileNotFoundException(string? message, string? fileLocation) : base(message)
    {
        FileLocation = fileLocation;
    }

    public DataFileNotFoundException(string? message, string? fileLocation, Exception? innerException) : base(message, innerException)
    {
        FileLocation = fileLocation;
    }
}