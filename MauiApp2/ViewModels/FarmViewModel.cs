using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp2.Models;
using MauiApp2.Services;
namespace MauiApp2.ViewModels;
[QueryProperty(nameof(Farm), "farm")]
public class FarmViewModel : BaseViewModel
{
    private readonly PondService _ponds;
    private readonly AuthService _auth;
    public Farm? Farm { get; set; }
    public ObservableCollection<Pond> Ponds { get; } = new();
    public string Name { get; set; } = "";
    public double Area { get; set; }
    public double Depth { get; set; }
    public string WaterType { get; set; } = "عذبة";
    public int FishCount { get; set; }
    public bool IsEditing { get; set; } = false;
    public Pond? SelectedPond { get; set; }
    public ICommand LoadCommand => new Command(async () => await LoadAsync());
    public ICommand AddCommand => new Command(async () => await AddAsync());
    public ICommand EditCommand => new Command<Pond>(p => StartEdit(p));
    public ICommand SaveEditCommand => new Command(async () => await SaveEditAsync());
    public ICommand DeleteCommand => new Command<Pond>(async p => await DeleteAsync(p));
    public ICommand OpenPondCommand => new Command<Pond>(async p => await OpenPondAsync(p));
    public FarmViewModel(PondService ponds, AuthService auth){ _ponds=ponds; _auth=auth; }
    public bool CanManage() => _auth.CurrentUser?.Role is UserRole.Admin or UserRole.Manager;
    public async Task LoadAsync()
    {
        if (Farm is null) return;
        Ponds.Clear();
        foreach (var p in await _ponds.GetByFarmAsync(Farm.Id)) Ponds.Add(p);
    }
    private async Task AddAsync()
    {
        if (!CanManage()) { await Application.Current.MainPage.DisplayAlert("صلاحيات","لا تملك صلاحية الإضافة","حسناً"); return; }
        if (string.IsNullOrWhiteSpace(Name)) { await Application.Current.MainPage.DisplayAlert("تنبيه","أدخل اسم الحوض","حسناً"); return; }
        if (Area<=0 || Depth<=0) { await Application.Current.MainPage.DisplayAlert("تنبيه","المساحة والعمق يجب أن تكون أكبر من صفر","حسناً"); return; }
        var p = new Pond{ FarmId=Farm!.Id, Name=Name, Area=Area, Depth=Depth, WaterType=WaterType, FishCount=FishCount };
        await _ponds.AddAsync(p);
        Name = ""; Area=0; Depth=0; WaterType="عذبة"; FishCount=0;
        await LoadAsync();
    }
    private void StartEdit(Pond p)
    {
        SelectedPond = p;
        Name = p.Name; Area=p.Area; Depth=p.Depth; WaterType=p.WaterType; FishCount=p.FishCount;
        IsEditing = true;
        OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(Area)); OnPropertyChanged(nameof(Depth));
        OnPropertyChanged(nameof(WaterType)); OnPropertyChanged(nameof(FishCount)); OnPropertyChanged(nameof(IsEditing));
    }
    private async Task SaveEditAsync()
    {
        if (SelectedPond is null) return;
        if (string.IsNullOrWhiteSpace(Name)) { await Application.Current.MainPage.DisplayAlert("تنبيه","أدخل اسم الحوض","حسناً"); return; }
        SelectedPond.Name=Name; SelectedPond.Area=Area; SelectedPond.Depth=Depth; SelectedPond.WaterType=WaterType; SelectedPond.FishCount=FishCount;
        await _ponds.UpdateAsync(SelectedPond);
        IsEditing=false; SelectedPond=null; Name=""; Area=0; Depth=0; WaterType="عذبة"; FishCount=0;
        OnPropertyChanged(nameof(IsEditing)); await LoadAsync();
    }
    private async Task DeleteAsync(Pond p)
    {
        if (await Application.Current.MainPage.DisplayAlert("حذف",
            $"حذف الحوض \"{p.Name}\"؟",
            "نعم", "لا"))
        { await _ponds.DeleteAsync(p.Id); await LoadAsync(); }
    }
    private async Task OpenPondAsync(Pond p)
        => await Shell.Current.GoToAsync(nameof(MauiApp2.Views.PondPage), true, new Dictionary<string, object>{{"pond", p}, {"farm", Farm!}});
}
