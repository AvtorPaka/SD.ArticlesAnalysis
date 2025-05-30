using System.Net;
using SD.ArticlesAnalysis.Storage.Infrastructure.DependencyInjection.Extensions;

namespace SD.ArticlesAnalysis.Storage.Api;

public sealed class Program
{
    public static async Task Main()
    {
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .ConfigureWebHost(webHostBuilder =>
            {
                webHostBuilder.ConfigureKestrel((context, kestrelOptions) =>
                    {
                        kestrelOptions.Listen(IPAddress.Any, 7070);
                    }
                );
            });

        await hostBuilder
            .Build()
            .MigrateUp()
            .RunAsync();
    }
}