using Microsoft.Extensions.DependencyInjection;
using SD.ArticlesAnalysis.Analysis.Domain.Services;
using SD.ArticlesAnalysis.Analysis.Domain.Services.Interfaces;

namespace SD.ArticlesAnalysis.Analysis.Domain.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IArticleAnalysisService, ArticleAnalysisService>();
        
        return services;
    }
}