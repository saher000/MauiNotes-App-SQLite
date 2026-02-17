using MauiNotesApp.Services;
using MauiNotesApp.ViewModels;
using MauiNotesApp.Views;


namespace MauiNotesApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder


        builder.Services.AddSingleton<NotesDatabase>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
