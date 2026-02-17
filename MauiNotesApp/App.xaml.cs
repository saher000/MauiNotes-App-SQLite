using MauiNotesApp.Views;
using Microsoft.Maui.Controls;

namespace MauiNotesApp;

public partial class App : Application
{
    public App(MainPage mainPage)
    {
        InitializeComponent();
        MainPage = new NavigationPage(mainPage);
    }
}
