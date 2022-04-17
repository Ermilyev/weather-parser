using System.Reflection;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace WebApi.Services;

internal static class Program
{
    internal static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        const string loggerTemplate = @"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u4}]<{ThreadId}> [{SourceContext:l}] {Message:lj}{NewLine}{Exception}";

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.With(new ThreadIdEnricher())
            .Enrich.FromLogContext()
            .WriteTo.Console(LogEventLevel.Information, loggerTemplate, theme: AnsiConsoleTheme.Literate)
            .CreateLogger();

        try
        {
            Log.Information("====================================================================");
            Log.Information("Application Starts. Version: {}", Assembly.GetEntryAssembly()?.GetName().Version);
            Log.Information("Application Directory: {}", AppDomain.CurrentDomain.BaseDirectory);
            await CreateHostBuilder(args).Build().RunAsync();
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Application terminated unexpectedly");
        }
        finally
        {
            Log.Information("====================================================================\r\n");
            Log.CloseAndFlush();
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}