namespace MauiApp2;
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Views.DashboardPage), typeof(Views.DashboardPage));
        Routing.RegisterRoute(nameof(Views.FarmPage), typeof(Views.FarmPage));
        Routing.RegisterRoute(nameof(Views.PondPage), typeof(Views.PondPage));
        Routing.RegisterRoute(nameof(Views.NotesPage), typeof(Views.NotesPage));
        Routing.RegisterRoute(nameof(Views.DiseaseSearchPage), typeof(Views.DiseaseSearchPage));
        GoToAsync("//LoginPage");
    }
}
