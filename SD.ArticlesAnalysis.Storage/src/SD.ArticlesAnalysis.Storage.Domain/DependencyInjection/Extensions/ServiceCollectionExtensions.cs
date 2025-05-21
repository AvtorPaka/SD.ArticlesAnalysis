using Microsoft.Extensions.DependencyInjection;
using SD.ArticlesAnalysis.Storage.Domain.Services;
using SD.ArticlesAnalysis.Storage.Domain.Services.Interfaces;

namespace SD.ArticlesAnalysis.Storage.Domain.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IArticlesTransferService, ArticlesTransferService>();
        services.AddScoped<IHasherService, HasherService>();

        return services;
    }
}