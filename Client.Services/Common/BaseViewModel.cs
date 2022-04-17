using System.ComponentModel;

namespace Client.Services.Common;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string property) => OnPropertyChanged(this, property);
    protected void OnPropertyChanged(object sender, string property) => PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(property));
}