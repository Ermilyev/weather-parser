using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Client.Models.City;
using Client.Services.Common;

namespace Client.Services.ViewModels;

public class MainViewModel : BaseViewModel
{
    #region Constructors

    public MainViewModel()
    {
        
    }
    #endregion
    
    #region Events
    private event EventHandler CitySelected;
    #endregion
    
    #region Fields
    private ObservableCollection<CityModel> cities;
    
    private CityModel city;
    #endregion
    
    #region Properties
    //private ExternalController Controller { get; }
    
    public ObservableCollection<CityModel> Cities
    {
        get { return cities; }
        set
        {
            cities = value;

            OnPropertyChanged(nameof(Cities));
        }
    }
    
    public CityModel City
    {
        get
        {
            return city;
        }
        set
        {
            city = value;

            OnPropertyChanged(nameof(City));

            CitySelected?.Invoke(this, EventArgs.Empty);
        }
    }
    #endregion
    
    #region Commands
    public ICommand UpdateCityCommand { get; }
    public ICommand CloseCityCommand { get; }
    #endregion
    #region Handlers
    #endregion
    #region Methods
    #endregion
}