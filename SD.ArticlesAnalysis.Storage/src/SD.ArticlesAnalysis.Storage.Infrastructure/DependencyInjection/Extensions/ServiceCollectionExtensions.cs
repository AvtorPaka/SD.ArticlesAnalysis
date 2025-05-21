using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SD.ArticlesAnalysis.Storage.Domain.Contracts.Dal.Interfaces;
using SD.ArticlesAnalysis.Storage.Infrastructure.Configuration.Options;
using SD.ArticlesAnalysis.Storage.Infrastructure.Dal.Infrastructure;
using SD.ArticlesAnalysis.Storage.Infrastructure.Dal.Repositories;

namespace SD.ArticlesAnalysis.Storage.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalInfrastructure(this IServiceCollection services,
        IConfiguration configuration, bool isDevelopment)
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

    public static IServiceCollection AddDalRepositories(this IServiceCollection services)
    {
        services.AddScoped<IArticlesMetaRepository, ArticlesMetaRepository>();
        services.AddScoped<IArticlesDataRepository, ArticlesDataRepository>();

        return services;
    }
}