using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp2.Models;
using MauiApp2.Services;
namespace MauiApp2.ViewModels;
public class DashboardViewModel : BaseViewModel
{
    private readonly AuthService _auth;
    private readonly FarmService _farms;
    public DashboardViewModel(AuthService auth, FarmService farms){ _auth=auth; _farms=farms; }
    public User? CurrentUser => _auth.CurrentUser;
    public ObservableCollection<Farm> Farms { get; } = new();
    public string FarmName { get; set; } = "";
    public string FarmLocation { get; set; } = "";
    public bool IsEditing { get; set; } = false;
    public Farm? SelectedFarm { get; set; }
    public ICommand LoadCommand => new Command(async () => await LoadAsync());
    public ICommand AddCommand => new Command(async () => await AddAsync());
    public ICommand EditCommand => new Command<Farm>(f => StartEdit(f));
    public ICommand SaveEditCommand => new Command(async () => await SaveEditAsync());
    public ICommand DeleteCommand => new Command<Farm>(async f => await DeleteAsync(f));
    public ICommand OpenFarmCommand => new Command<Farm>(async f => await OpenFarmAsync(f));
    public ICommand LogoutCommand => new Command(async () => await Shell.Current.GoToAsync("//LoginPage"));
    public async Task LoadAsync()
    {
        Farms.Clear();
        foreach (var f in await _farms.GetAllAsync()) Farms.Add(f);
        OnPropertyChanged(nameof(CurrentUser));
    }
    private async Task AddAsync()
    {
        if (string.IsNullOrWhiteSpace(FarmName))
        { await Application.Current.MainPage.DisplayAlert("تنبيه","يجب إدخال اسم المزرعة","حسناً"); return; }
        if (string.IsNullOrWhiteSpace(FarmLocation))
        { await Application.Current.MainPage.DisplayAlert("تنبيه","يجب إدخال موقع المزرعة","حسناً"); return; }
        await _farms.AddAsync(new Farm{ Name=FarmName, Location=FarmLocation });
        FarmName=""; FarmLocation=""; await LoadAsync();
    }
    private void StartEdit(Farm f)
    {
        SelectedFarm = f;
        FarmName = f.Name; FarmLocation = f.Location;
        IsEditing = true;
        OnPropertyChanged(nameof(FarmName)); OnPropertyChanged(nameof(FarmLocation)); OnPropertyChanged(nameof(IsEditing));
    }
    private async Task SaveEditAsync()
    {
        if (SelectedFarm is null) return;
        if (string.IsNullOrWhiteSpace(FarmName) || string.IsNullOrWhiteSpace(FarmLocation))
        { await Application.Current.MainPage.DisplayAlert("تنبيه","أكمل الحقول المطلوبة","حسناً"); return; }
        SelectedFarm.Name = FarmName; SelectedFarm.Location = FarmLocation;
        await _farms.UpdateAsync(SelectedFarm);
        IsEditing = false; SelectedFarm = null; FarmName=""; FarmLocation="";
        OnPropertyChanged(nameof(IsEditing)); await LoadAsync();
    }
    private async Task DeleteAsync(Farm f)
    {
        if (await Application.Current.MainPage.DisplayAlert("حذف",
            $"حذف الحوض \"{f.Name}\"؟",
            "نعم", "لا"))
        { await _farms.DeleteAsync(f.Id); await LoadAsync(); }
    }
    private async Task OpenFarmAsync(Farm f)
        => await Shell.Current.GoToAsync(nameof(MauiApp2.Views.FarmPage), true, new Dictionary<string, object>{{"farm", f}});
}
