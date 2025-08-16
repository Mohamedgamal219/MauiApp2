using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace MauiApp2.ViewModels;
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    bool isBusy;
    public bool IsBusy { get => isBusy; set { isBusy = value; OnPropertyChanged(); } }
}
