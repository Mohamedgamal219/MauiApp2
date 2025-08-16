using MauiApp2.ViewModels;
using MauiApp2.Models;
namespace MauiApp2.Views;
public partial class PondPage : ContentPage, IQueryAttributable
{
    private readonly PondViewModel _vm;
    public PondPage(PondViewModel vm){ InitializeComponent(); _vm=vm; BindingContext=vm; }
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("pond", out var p) && p is Pond pond) _vm.Pond = pond;
        if (query.TryGetValue("farm", out var f) && f is Farm farm) _vm.Farm = farm;
        await _vm.LoadAsync();
    }
}
