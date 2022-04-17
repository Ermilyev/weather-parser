using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Parser.Services.Services;

namespace Parser.Services;

public class Worker : BackgroundService
{
    private static readonly TimeSpan DelaySpan = TimeSpan.FromHours(24);
    
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Factory.StartNew(
            () => RunAsync(stoppingToken),
            stoppingToken,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);

        return Task.CompletedTask;
    }

    private async Task RunAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation(
                    "Worker Starting | Service: {Service}, Method: {Method}, Time - {Datetime}",  
                    nameof(Worker),
                    nameof(RunAsync),
                    DateTimeOffset.Now);
                
                using (var scope = _serviceScopeFactory.CreateScope())
                { 
                    var (parserScopeService, 
                         restScopeService) = 
                        (scope.ServiceProvider.GetRequiredService<ParserService>(), 
                         scope.ServiceProvider.GetRequiredService<RestService>());

                    var weathers = await parserScopeService.ParseAsync();

                    if (weathers.Any())
                        await restScopeService.WorkAsync(weathers);
                }

                _logger.LogInformation(
                    "Worker Finished | Service: {Service}, Method: {Method}, Time - {Datetime}",  
                    nameof(Worker),
                    nameof(RunAsync),
                    DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Worker Error: {Error},Service: {Service}, Method: {Method}, Time - {Datetime}",  
                    ex.Message,
                    nameof(Worker),
                    nameof(RunAsync),
                    DateTimeOffset.Now);
            }
            
            await Task.Delay(DelaySpan, stoppingToken);
        }
    }
}