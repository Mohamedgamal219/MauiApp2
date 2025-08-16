using System.Collections.ObjectModel;
using MauiApp2.Models;
using MauiApp2.Services;
namespace MauiApp2.ViewModels;
[QueryProperty(nameof(Pond), "pond")]
public class NotesViewModel : BaseViewModel
{
    private readonly NoteService _notes;
    public Pond? Pond { get; set; }
    public ObservableCollection<Note> Items { get; } = new();
    public NotesViewModel(NoteService notes){ _notes=notes; }
    public async Task LoadAsync()
    {
        Items.Clear();
        if (Pond is null) return;
        foreach (var n in await _notes.GetByPondAsync(Pond.Id)) Items.Add(n);
    }
}
