using MauiApp2.ViewModels;
namespace MauiApp2.Views;
public partial class DashboardPage : ContentPage
{
    private readonly DashboardViewModel _vm;
    public DashboardPage(DashboardViewModel vm){ InitializeComponent(); _vm=vm; BindingContext=vm; }
    protected override async void OnAppearing(){ base.OnAppearing(); await _vm.LoadAsync(); }
    private async void OnOpenDiseaseSearch(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(DiseaseSearchPage));
}
