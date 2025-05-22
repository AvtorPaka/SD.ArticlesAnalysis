namespace SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Dto.Request;

public record GetArticleWordCloudRequest(
    string Text,
    string Format,
    int Width,
    int Height,
    string FontFamily,
    int FontScale
);