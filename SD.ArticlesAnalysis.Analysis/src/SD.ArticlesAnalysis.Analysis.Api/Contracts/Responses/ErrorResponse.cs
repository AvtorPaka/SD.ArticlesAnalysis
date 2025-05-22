using System.Net;

namespace SD.ArticlesAnalysis.Analysis.Api.Contracts.Responses;

public record ErrorResponse(
    HttpStatusCode StatusCode,
    string? Message
);