using System.Windows;
using Client.Domain.Repositories.Store;
using Client.Infrastructure.Repositories.Store;
using Client.Services.Controllers;
using Client.Services.Views;
using Common.Infrastructure.Utility.RestSharp;
using Common.Infrastructure.Utility.RestSharp.Store;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Client.Services
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    ConfigureServices(services);
                })
                .Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddTransient<CityController>();
            services.AddTransient<WeatherController>();
            services.AddTransient<ForecastDateController>();
            services.AddTransient<ICityRepository,CityRepository>();
            services.AddTransient<IWeatherRepository,WeatherRepository>();
            services.AddTransient<IForecastDateRepository,ForecastDateRepository>();
            services.AddTransient<IRestRepository,RestRepository>();
            services.AddSingleton(Log.Logger);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
            
            base.OnStartup(e);
        }
        
        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }
            
            base.OnExit(e);
        }
    }
}