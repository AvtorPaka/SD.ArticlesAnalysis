using System.Text.Json;

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
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            })
            .AddMvcOptions(o =>
            {
                // o.Filters.Add<ExceptionFilter>();
            });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UsePathBase("/api/storage");
        app.UseRouting();

        app.UseEndpoints(builder =>
        {
            builder.MapControllers();
        });
    }
}