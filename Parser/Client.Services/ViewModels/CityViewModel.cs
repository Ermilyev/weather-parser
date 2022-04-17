using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Client.Models.City;
using Client.Services.Controllers;

namespace Client.Services.ViewModels;

public class CityViewModel : ViewModelBase
{
   private readonly CityController _cityController;
   
   private readonly ObservableCollection<CityModel>? _cities;
   public IEnumerable<CityModel>? Cities => _cities;
   public ICommand LoadCityList { get; }

   public CityViewModel(CityController cityController)
   {
      _cityController = cityController;
   }
}