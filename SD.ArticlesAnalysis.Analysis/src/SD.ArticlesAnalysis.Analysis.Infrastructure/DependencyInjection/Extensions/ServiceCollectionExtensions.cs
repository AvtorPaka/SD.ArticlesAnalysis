using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Dal.Interfaces;
using SD.ArticlesAnalysis.Analysis.Domain.Contracts.Isc.Interfaces;
using SD.ArticlesAnalysis.Analysis.Infrastructure.Configuration.Options;
using SD.ArticlesAnalysis.Analysis.Infrastructure.Dal.Infrastructure;
using SD.ArticlesAnalysis.Analysis.Infrastructure.Dal.Repositories;
using SD.ArticlesAnalysis.Analysis.Infrastructure.Isc.Clients;

namespace SD.ArticlesAnalysis.Analysis.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    private const string StorageClientAddressEnvKey = "SD_AA_STORAGE_URL";

    public static IServiceCollection AddDalInfrastructure(this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment)
    {
        services.AddInfrastructureConfigurations(configuration);

        var postgreConnectionSection =
            configuration.GetSection($"Infrastructure:Dal:{nameof(PostgreConnectionOptions)}");

        PostgreConnectionOptions pgConnectionOptions =
            postgreConnectionSection.Get<PostgreConnectionOptions>() ??
            throw new ArgumentException("PostgreSQL connection options are missing");

        Postgres.ConfigureTypeMapOptions();

        Postgres.AddMigrations(
            services: services,
            connectionOptions: pgConnectionOptions
        );

        Postgres.AddDataSource(
            services: services,
            connectionOptions: pgConnectionOptions,
            isDevelopment: isDevelopment
        );


        return services;
    }

    private static void AddInfrastructureConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileStorageLocationOptions>(
            configuration.GetSection($"Infrastructure:Dal:{nameof(FileStorageLocationOptions)}"));
    }

    public static IServiceCollection AddStorageServiceClient(this IServiceCollection services,
        IConfiguration configuration, bool isDevelopment)
    {
        services.AddSingleton<IArticleStorageServiceClient, ArticleStorageServiceClient>();


        string storageServiceBaseAddress = ResolveStorageClientBaseAddress(
            configuration: configuration,
            isDevelopment: isDevelopment
        );

        services.AddHttpClient(
                ArticleStorageServiceClient.ArticleStorageClientTag,
                client => { client.BaseAddress = new Uri($"{storageServiceBaseAddress}/api/storage"); }
        ).AddTransientHttpErrorPolicy(builder =>
                builder.WaitAndRetryAsync(3, retryTime => TimeSpan.FromMilliseconds(500)));

        return services;
    }

    public static IServiceCollection AddWordCloudApiClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        var wordCloudApiConfSection = configuration.GetSection("Infrastructure:ISC:WordCloud");

        string wcApiBaseUrl = wordCloudApiConfSection.GetValue<string>("ApiBaseUrl") ??
                              throw new ArgumentException("Word Cloud api address is missing");

        services.AddSingleton<IWordCloudApiClient, WordCloudApiClient>();

        services.AddHttpClient(
            WordCloudApiClient.WordCloudApiClientTag,
            client => { client.BaseAddress = new Uri(wcApiBaseUrl); }
        ).AddTransientHttpErrorPolicy(builder =>
            builder.WaitAndRetryAsync(2, retryTime => TimeSpan.FromMilliseconds(1000)));

        return services;
    }

    private static string ResolveStorageClientBaseAddress(IConfiguration configuration, bool isDevelopment)
    {
        if (isDevelopment)
        {
            var storageClientAddressSection = configuration.GetSection("Infrastructure:ISC:SD.AA.Storage");

            return storageClientAddressSection.GetValue<string>("ApiBaseUrl") ??
                   throw new ArgumentException("SD.AA.Storage address is missing");
        }

        return Environment.GetEnvironmentVariable(StorageClientAddressEnvKey) ??
               throw new ArgumentException("SD.AA.Storage address is missing");
    }

    public static IServiceCollection AddDalRepositories(this IServiceCollection services)
    {
        services.AddScoped<IArticleAnalysisRepository, ArticleAnalysisRepository>();
        services.AddScoped<IArticleWordCloudRepository, ArticleWordCloudRepository>();

        return services;
    }
}