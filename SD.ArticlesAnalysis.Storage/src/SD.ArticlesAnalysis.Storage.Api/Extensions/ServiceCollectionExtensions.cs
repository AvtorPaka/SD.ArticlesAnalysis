using SD.ArticlesAnalysis.Storage.Api.Filters;

namespace SD.ArticlesAnalysis.Storage.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddGlobalFilters(this IServiceCollection services)
    {
        services.AddScoped<ExceptionFilter>();

        return services;
    }
}