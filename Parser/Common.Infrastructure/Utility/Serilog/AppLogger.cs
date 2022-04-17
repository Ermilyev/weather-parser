using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using ILogger = Serilog.ILogger;

namespace Common.Infrastructure.Utility.Serilog;

public class AppLogger
{
    private static readonly ILogger _perfLogger;
    private static readonly ILogger _usageLogger;
    private static readonly ILogger _errorLogger;
    private static readonly ILogger _diagnosticLogger;

    private const string LoggerTemplate = @"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u4}]<{ThreadId}> [{SourceContext:l}] {Message:lj}{NewLine}{Exception}";

    static AppLogger()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _perfLogger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.With(new ThreadIdEnricher())
            .Enrich.FromLogContext()
            .WriteTo.Console(LogEventLevel.Information, LoggerTemplate, theme: AnsiConsoleTheme.Literate)
            .CreateLogger();

        _usageLogger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.With(new ThreadIdEnricher())
            .Enrich.FromLogContext()
            .WriteTo.Console(LogEventLevel.Information, LoggerTemplate, theme: AnsiConsoleTheme.Literate)
            .CreateLogger();

        _errorLogger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.With(new ThreadIdEnricher())
            .Enrich.FromLogContext()
            .WriteTo.Console(LogEventLevel.Error, LoggerTemplate, theme: AnsiConsoleTheme.Literate)
            .CreateLogger();

        _diagnosticLogger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.With(new ThreadIdEnricher())
            .Enrich.FromLogContext()
            .WriteTo.Console(LogEventLevel.Information, LoggerTemplate, theme: AnsiConsoleTheme.Literate)
            .CreateLogger();
    }

    public static void WritePerf(LogDetail infoToLog)
    {
        _perfLogger.Write(LogEventLevel.Information, "{@LogDetail}", infoToLog);
    }

    public static void WriteUsage(LogDetail infoToLog)
    {
        _usageLogger.Write(LogEventLevel.Information, "{@LogDetail}", infoToLog);
    }

    public static void WriteError(LogDetail infoToLog)
    {
        var procName = FindProcName(infoToLog.Exception);
        infoToLog.Location = string.IsNullOrEmpty(procName) ? infoToLog.Location : procName;
        infoToLog.Message = GetMessageFromException(infoToLog.Exception);
        _errorLogger.Write(LogEventLevel.Error, "{@LogDetail}", infoToLog);
    }

    public static void WriteDiagnostic(LogDetail infoToLog)
    {
        var writeDiagnostics =
                Convert.ToBoolean(Environment.GetEnvironmentVariable("DIAGNOSTICS_ON"));
        if (!writeDiagnostics)
            return;

        _diagnosticLogger.Write(LogEventLevel.Information, "{@LogDetail}", infoToLog);
    }

    private static string GetMessageFromException(Exception ex)
    {
        while (true)
        {
            if (ex.InnerException == null) 
                return ex.Message;

            ex = ex.InnerException;
        }
    }

    private static string FindProcName(Exception ex)
    {
        while (true)
        {
            if (ex.InnerException == null) 
                return string.Empty;

            ex = ex.InnerException;
        }
    }
}
