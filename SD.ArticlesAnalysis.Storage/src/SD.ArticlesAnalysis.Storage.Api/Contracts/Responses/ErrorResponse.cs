using System.Net;

namespace SD.ArticlesAnalysis.Storage.Api.Contracts.Responses;

public record ErrorResponse(
    HttpStatusCode StatusCode,
    string? Message
);