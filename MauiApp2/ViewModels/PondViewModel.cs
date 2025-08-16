using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp2.Models;
using MauiApp2.Services;
namespace MauiApp2.ViewModels;
[QueryProperty(nameof(Pond), "pond")]
[QueryProperty(nameof(Farm), "farm")]
public class PondViewModel : BaseViewModel
{
    private readonly NoteService _notes;
    private readonly ExcelExportService _excel;
    private readonly AuthService _auth;
    public Farm? Farm { get; set; }
    public Pond? Pond { get; set; }
    public ObservableCollection<Note> Notes { get; } = new();
    public DateTime Date { get; set; } = DateTime.Now;
    public double Temperature { get; set; }
    public string Feeding { get; set; } = "";
    public int Mortality { get; set; }
    public double PH { get; set; }
    public double DissolvedOxygen { get; set; }
    public double Ammonia { get; set; }
    public double Salinity { get; set; }
    public string Comment { get; set; } = "";
    public bool IsEditing { get; set; } = false;
    public Note? SelectedNote { get; set; }
    public ICommand LoadCommand => new Command(async () => await LoadAsync());
    public ICommand AddCommand => new Command(async () => await AddAsync());
    public ICommand EditCommand => new Command<Note>(n => StartEdit(n));
    public ICommand SaveEditCommand => new Command(async () => await SaveEditAsync());
    public ICommand DeleteCommand => new Command<Note>(async n => await DeleteAsync(n));
    public ICommand ExportCommand => new Command(async () => await ExportAsync());
    public PondViewModel(NoteService notes, ExcelExportService excel, AuthService auth){ _notes=notes; _excel=excel; _auth=auth; }
    public bool CanAddNotes() => _auth.CurrentUser?.Role is UserRole.Admin or UserRole.Manager or UserRole.Engineer;
    public async Task LoadAsync()
    {
        if (Pond is null) return;
        Notes.Clear();
        foreach (var n in await _notes.GetByPondAsync(Pond.Id)) Notes.Add(n);
    }
    private async Task AddAsync()
    {
        if (!CanAddNotes()){ await Application.Current.MainPage.DisplayAlert("صلاحيات","لا تملك صلاحية الإضافة","حسناً"); return; }
        if (Pond is null) return;
        if (Temperature==0 && PH==0 && DissolvedOxygen==0 && Ammonia==0 && Salinity==0 && string.IsNullOrWhiteSpace(Feeding) && Mortality==0 && string.IsNullOrWhiteSpace(Comment))
        { await Application.Current.MainPage.DisplayAlert("تنبيه","أدخل بيانات الملاحظة قبل الحفظ","حسناً"); return; }
        var n = new Note{
            PondId=Pond.Id, Date=Date, Temperature=Temperature, Feeding=Feeding, Mortality=Mortality,
            PH=PH, DissolvedOxygen=DissolvedOxygen, Ammonia=Ammonia, Salinity=Salinity, Comment=Comment
        };
        await _notes.AddAsync(n);
        Date = DateTime.Now; Temperature=0; Feeding=""; Mortality=0; PH=0; DissolvedOxygen=0; Ammonia=0; Salinity=0; Comment="";
        OnPropertyChanged(nameof(Date)); OnPropertyChanged(nameof(Temperature)); OnPropertyChanged(nameof(Feeding)); OnPropertyChanged(nameof(Mortality));
        OnPropertyChanged(nameof(PH)); OnPropertyChanged(nameof(DissolvedOxygen)); OnPropertyChanged(nameof(Ammonia)); OnPropertyChanged(nameof(Salinity)); OnPropertyChanged(nameof(Comment));
        await LoadAsync();
    }
    private void StartEdit(Note n)
    {
        SelectedNote = n;
        Date=n.Date; Temperature=n.Temperature; Feeding=n.Feeding; Mortality=n.Mortality; PH=n.PH; DissolvedOxygen=n.DissolvedOxygen; Ammonia=n.Ammonia; Salinity=n.Salinity; Comment=n.Comment;
        IsEditing = true; OnPropertyChanged(nameof(IsEditing));
    }
    private async Task SaveEditAsync()
    {
        if (SelectedNote is null) return;
        SelectedNote.Date=Date; SelectedNote.Temperature=Temperature; SelectedNote.Feeding=Feeding; SelectedNote.Mortality=Mortality;
        SelectedNote.PH=PH; SelectedNote.DissolvedOxygen=DissolvedOxygen; SelectedNote.Ammonia=Ammonia; SelectedNote.Salinity=Salinity; SelectedNote.Comment=Comment;
        await _notes.UpdateAsync(SelectedNote);
        IsEditing=false; SelectedNote=null; await LoadAsync();
    }
    private async Task DeleteAsync(Note n)
    {
        if (await Application.Current.MainPage.DisplayAlert("حذف","حذف هذه الملاحظة؟","نعم","لا"))
        { await _notes.DeleteAsync(n.Id); await LoadAsync(); }
    }
    private async Task ExportAsync()
    {
        if (Farm is null || Pond is null) return;
        var path = await _excel.ExportPondAsync(Farm, Pond, Notes.ToList());
        await Application.Current.MainPage.DisplayAlert("تم التصدير", $"تم حفظ ملف Excel في:\n{path}", "حسناً");
    }
}
