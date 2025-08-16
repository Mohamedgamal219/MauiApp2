using Microsoft.Extensions.Logging;
using MauiApp2.Services;
using MauiApp2.ViewModels;
using MauiApp2.Views;

namespace MauiApp2;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        // Services
        builder.Services.AddSingleton<AuthService>();
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<FarmService>();
        builder.Services.AddSingleton<PondService>();
        builder.Services.AddSingleton<NoteService>();
        builder.Services.AddSingleton<DiseaseSearchService>();
        builder.Services.AddSingleton<ExcelExportService>();

        // ViewModels
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<DashboardViewModel>();
        builder.Services.AddTransient<FarmViewModel>();
        builder.Services.AddTransient<PondViewModel>();
        builder.Services.AddTransient<NotesViewModel>();
        builder.Services.AddTransient<DiseaseSearchViewModel>();

        // Views
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<FarmPage>();
        builder.Services.AddTransient<PondPage>();
        builder.Services.AddTransient<NotesPage>();
        builder.Services.AddTransient<DiseaseSearchPage>();

        return builder.Build();
    }
}
