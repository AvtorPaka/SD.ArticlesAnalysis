using System.Text.Json;
using SD.ArticlesAnalysis.Analysis.Api.Extensions;
using SD.ArticlesAnalysis.Analysis.Api.Filters;
using SD.ArticlesAnalysis.Analysis.Api.Middleware;
using SD.ArticlesAnalysis.Analysis.Domain.DependencyInjection.Extensions;
using SD.ArticlesAnalysis.Analysis.Infrastructure.DependencyInjection.Extensions;

namespace SD.ArticlesAnalysis.Analysis.Api;

internal sealed class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _hostEnvironment;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _hostEnvironment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddGlobalFilters()
            .AddDalInfrastructure
            (
                configuration: _configuration,
                isDevelopment: _hostEnvironment.IsDevelopment()
            )
            .AddDalRepositories()
            .AddDomainServices()
            .AddControllers()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            })
            .AddMvcOptions(o =>
            {
                o.Filters.Add<ExceptionFiler>();
            });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseMiddleware<TracingMiddleware>();
        app.UsePathBase("/api/analysis");
        app.UseRouting();

        app.UseMiddleware<LoggingMiddleware>();

        app.UseEndpoints(builder =>
        {
            builder.MapControllers();
        });
    }
}