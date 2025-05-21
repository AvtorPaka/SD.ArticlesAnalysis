using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SD.ArticlesAnalysis.Storage.Infrastructure.DependencyInjection.Extensions;

public static class HostExtensions
{
    public static IHost MigrateUp(this IHost host)
    {
        var scope = host.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();

        return host;
    }
    
    public static IHost MigrateDown(this IHost host, long version = 20250521)
    {
        var scope = host.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateDown(version);

        return host;
    }
}