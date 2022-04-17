using System;
using Client.Services.Common;

namespace Client.Services.Models;

public class CityModel : BaseViewModel
{
    #region Constructors

    public CityModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    public CityModel()
    {
    }
    #endregion
    
    #region Fields
    private Guid id;
    private string name;
    #endregion
    
    #region Properties
    public Guid Id
    {
        get { return id; }
        set
        {
            id = value;

            OnPropertyChanged(this, nameof(Id));
        }
    }
    public string Name
    {
        get { return name; }
        set
        {
            name = value;

            OnPropertyChanged(this, nameof(Name));
        }
    }
    #endregion
    
    #region Overriding
    public override string ToString()
    {
        return $"{Id} {Name}";
    }
    #endregion
}