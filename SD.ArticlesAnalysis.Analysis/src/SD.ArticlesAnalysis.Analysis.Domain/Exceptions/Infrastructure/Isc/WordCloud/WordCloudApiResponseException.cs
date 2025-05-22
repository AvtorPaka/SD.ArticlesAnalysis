using System.Net;

namespace SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Isc.WordCloud;

public class WordCloudApiResponseException: InfrastructureException
{
    public HttpStatusCode StatusCode { get; init; }
    public Uri? Address { get; init; }
    
    public WordCloudApiResponseException(string? message, HttpStatusCode statusCode, Uri? address) : base(message)
    {
        StatusCode = statusCode;
        Address = address;
    }
}