using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp2.Models;
using MauiApp2.Services;
namespace MauiApp2.ViewModels;
public class DiseaseSearchViewModel : BaseViewModel
{
    private readonly DiseaseSearchService _service;
    public string Query { get; set; } = "";
    public ObservableCollection<FishDisease> Results { get; } = new();
    public ICommand SearchCommand { get; }
    public DiseaseSearchViewModel(DiseaseSearchService service)
    {
        _service = service;
        SearchCommand = new Command(async () => await SearchAsync());
    }
    private async Task SearchAsync()
    {
        Results.Clear();
        if (string.IsNullOrWhiteSpace(Query)) return;
        var items = await _service.SearchAsync(Query);
        foreach (var i in items) Results.Add(i);
    }
}
