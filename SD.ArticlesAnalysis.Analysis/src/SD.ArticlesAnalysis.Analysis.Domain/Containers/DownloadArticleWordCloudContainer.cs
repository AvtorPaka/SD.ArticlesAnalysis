namespace SD.ArticlesAnalysis.Analysis.Domain.Containers;

public record DownloadArticleWordCloudContainer(
    Stream WordCloudFileStream,
    string DispositionFilename
);