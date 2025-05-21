namespace SD.ArticlesAnalysis.Storage.Domain.Containers;

public record DownloadArticleContainer(
    Stream ArticleFileStream,
    string? DispositionFilename
);