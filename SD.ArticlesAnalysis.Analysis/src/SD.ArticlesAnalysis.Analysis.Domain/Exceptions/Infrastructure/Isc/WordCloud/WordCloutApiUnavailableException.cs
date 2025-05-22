namespace SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Isc.WordCloud;

public class WordCloutApiUnavailableException : InfrastructureException
{
    public Uri? Address { get; init; }

    public WordCloutApiUnavailableException(string? message, Uri? address, HttpRequestException? innerException) : base(
        message, innerException)
    {
        Address = address;
    }
}