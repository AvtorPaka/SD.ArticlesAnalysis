using SD.ArticlesAnalysis.Analysis.Api.Filters;

namespace SD.ArticlesAnalysis.Analysis.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddGlobalFilters(this IServiceCollection services)
    {
        services.AddScoped<ExceptionFiler>();
        return services;
    }
}