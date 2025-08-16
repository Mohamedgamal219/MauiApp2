using MauiApp2.ViewModels;
using MauiApp2.Models;
namespace MauiApp2.Views;
public partial class FarmPage : ContentPage, IQueryAttributable
{
    private readonly FarmViewModel _vm;
    public FarmPage(FarmViewModel vm){ InitializeComponent(); _vm=vm; BindingContext=vm; }
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("farm", out var f) && f is Farm farm){ _vm.Farm = farm; await _vm.LoadAsync(); }
    }
}
