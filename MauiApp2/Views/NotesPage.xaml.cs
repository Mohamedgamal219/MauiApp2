using MauiApp2.ViewModels;
using MauiApp2.Models;
namespace MauiApp2.Views;
public partial class NotesPage : ContentPage, IQueryAttributable
{
    private readonly NotesViewModel _vm;
    public NotesPage(NotesViewModel vm){ InitializeComponent(); _vm=vm; BindingContext=vm; }
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("pond", out var p) && p is Pond pond) _vm.Pond = pond;
        await _vm.LoadAsync();
    }
}
