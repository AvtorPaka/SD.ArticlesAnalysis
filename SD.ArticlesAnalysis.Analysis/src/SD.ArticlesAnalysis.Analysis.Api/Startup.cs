using System.Text.Json;

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
            .AddControllers()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            })
            .AddMvcOptions(o =>
            {
                // o.Filters.Add<ExceptionFilter>();
            });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UsePathBase("/api/analysis");
        app.UseRouting();

        app.UseEndpoints(builder =>
        {
            builder.MapControllers();
        });
    }
}