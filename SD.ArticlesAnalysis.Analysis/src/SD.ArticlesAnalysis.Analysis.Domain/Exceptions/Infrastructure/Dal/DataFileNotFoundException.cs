namespace SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Dal;

public class DataFileNotFoundException: InfrastructureException
{
    public string? FileLocation { get; init; }
    
    public DataFileNotFoundException(string? message, string? fileLocation) : base(message)
    {
        FileLocation = fileLocation;
    }

    public DataFileNotFoundException(string? message, string? fileLocation, Exception? innerException) : base(message, innerException)
    {
        FileLocation = fileLocation;
    }
}