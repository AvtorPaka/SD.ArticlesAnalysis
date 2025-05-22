using System.Net;

namespace SD.ArticlesAnalysis.Analysis.Domain.Exceptions.Infrastructure.Isc.Storage;

public class StorageServiceResponseException: InfrastructureException
{
    public HttpStatusCode StatusCode { get; init; }
    public Uri? Address { get; init; }
    public string? TraceId { get; init; }
    
    public StorageServiceResponseException(string? message, HttpStatusCode statusCode, Uri? address, string? traceId) : base(message)
    {
        StatusCode = statusCode;
        Address = address;
        TraceId = traceId;
    }
}