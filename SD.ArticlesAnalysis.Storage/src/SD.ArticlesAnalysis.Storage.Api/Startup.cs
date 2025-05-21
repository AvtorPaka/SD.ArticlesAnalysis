using System.Text.Json;
using FluentValidation;
using SD.ArticlesAnalysis.Storage.Api.Extensions;
using SD.ArticlesAnalysis.Storage.Api.Filters;
using SD.ArticlesAnalysis.Storage.Api.MiddleWare;
using SD.ArticlesAnalysis.Storage.Domain.DependencyInjection.Extensions;
using SD.ArticlesAnalysis.Storage.Infrastructure.DependencyInjection.Extensions;

namespace SD.ArticlesAnalysis.Storage.Api;

internal class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _hostEnvironment;

    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        _hostEnvironment = webHostEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddGlobalFilters()
            .AddDalInfrastructure(
                configuration: _configuration,
                isDevelopment: _hostEnvironment.IsDevelopment()
            )
            .AddDalRepositories()
            .AddDomainServices()
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            })
            .AddMvcOptions(o =>
            {
                o.Filters.Add<ExceptionFilter>();
            });
    }

    public void Configure(IApplicationBuilder app)
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        app.UseMiddleware<TracingMiddleware>();
        app.UsePathBase("/api/storage");
        app.UseRouting();

        app.UseMiddleware<LoggingMiddleware>();

        app.UseEndpoints(builder =>
        {
            builder.MapControllers();
        });
    }
}