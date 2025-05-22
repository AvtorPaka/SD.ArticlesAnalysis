using System.Net;

namespace SD.ArticlesAnalysis.Analysis.Api;

public sealed class Program
{
    public static async Task Main()
    {
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder.ConfigureKestrel((context, serverOptions) =>
                {
                    serverOptions.Listen(IPAddress.Any, 8080);
                });
            });

        await hostBuilder
            .Build()
            .RunAsync();
    }
}