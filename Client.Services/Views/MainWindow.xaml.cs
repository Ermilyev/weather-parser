using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using Client.Models.City;
using Client.Models.Weather;
using Client.Services.Controllers;

namespace Client.Services.Views;

public partial class MainWindow : Window
{
    private readonly CityController _cityController;
    private readonly ForecastDateController _forecastDateController;
    private readonly WeatherController _weatherController;

    private List<Guid> cityIds = new();
    private List<Guid> forecastDates = new();
    public MainWindow(CityController cityController, 
                      ForecastDateController forecastDateController, 
                      WeatherController weatherController)
    {
        _cityController = cityController;
        _forecastDateController = forecastDateController;
        _weatherController = weatherController;
        DataContext = this;
        
        InitializeComponent();
        Loading().GetAwaiter();
    }

    private async Task Loading()
    {
        await CitiesComboboxPopulate();
        await WeatherGridViewPopulate();
    }

    private async Task CitiesComboboxPopulate()
    {
        var cities = new List<CityModel>
        {
            new(Guid.Empty, "<не выбрано>")
        };
        
        var result = await _cityController.GetByFilter();
        if (result.CityModelList.Any())
            cities.AddRange(result.CityModelList);
        
        CmbCities.ItemsSource = cities.OrderBy(x => x.Name);
        CmbCities.DisplayMemberPath = "Name";
        CmbCities.SelectedIndex = 0;
    }
    
    private async Task WeatherGridViewPopulate(Guid[]? cityIds = null, Guid[]? forecastDates = null)
    {
        var weathers = new List<WeatherModel>();
        
        var result = await _weatherController.GetByFilter(0,20,null,cityIds,forecastDates);
        
        if (result.WeatherModels.Any())
            weathers.AddRange(result.WeatherModels);

        var newWeathers = new List<WeatherForLoad>();

        foreach (var weather in weathers)
        {
            var city = await _cityController.GetCity(weather.CityId);
            var date = await _forecastDateController.GetForecastDate(weather.ForecastDateId);
            var weathersForLoad = new WeatherForLoad()
            {
                Id = weather.Id,
                CityId = weather.CityId,
                ForecastDateId = weather.ForecastDateId,
                City = city.Name,
                Date = date.Date.ToString("d"),
                MinTempCelsius = weather.MinTempCelsius,
                MinTempFahrenheit = weather.MinTempFahrenheit,
                MaxTempCelsius = weather.MaxTempCelsius,
                MaxTempFahrenheit = weather.MaxTempFahrenheit,
                MaxWindSpeedMetersPerSecond = weather.MaxWindSpeedMetersPerSecond,
                MaxWindSpeedMilesPerHour = weather.MaxWindSpeedMilesPerHour,
            };
            
            newWeathers.Add(weathersForLoad);
        }
        
        WeatherDataGrid.ItemsSource = newWeathers.OrderBy(x=>x.Date).ThenBy(x => x.City);
    }

    private void CmbCities_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var city = (CityModel) CmbCities.SelectedValue;
        var cityId = city.Id;
        if (cityId == Guid.Empty)
            WeatherGridViewPopulate().AsResult();
        else
        {
            cityIds.Add(cityId);
            WeatherGridViewPopulate(cityIds.ToArray(), forecastDates.ToArray()).AsResult();
        }
    }
    
    private async void DatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        var dates = new List<DateTime>();
        if (ForecastDatePicker.SelectedDate != null) 
            dates.Add(ForecastDatePicker.SelectedDate.Value);
        var datesFromApi = await _forecastDateController.GetByFilter(dates.ToArray());
        
        forecastDates.Clear();
        foreach (var date in datesFromApi)
        {
            forecastDates.Add(date.Id);
        }

        WeatherGridViewPopulate(cityIds.ToArray(), forecastDates.ToArray()).AsResult();
    }

    public class WeatherForLoad
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public Guid ForecastDateId { get; set; }
        public string City { get; set; }
        public string Date { get; set; }
        public short MinTempCelsius { get; set; }
        public short MinTempFahrenheit { get; set; }
        public short MaxTempCelsius { get; set; }
        public short MaxTempFahrenheit { get; set; }
        public ushort MaxWindSpeedMetersPerSecond { get; set; }
        public ushort MaxWindSpeedMilesPerHour { get; set; }
    }
}