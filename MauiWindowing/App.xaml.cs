using MauiWindowing.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Application = Microsoft.Maui.Controls.Application;

#nullable enable

namespace MauiWindowing;

public partial class App : Application
{
    ContentPage page;
    private IDesktopEnvironmentService? windowHelperService;

    public App(MainPage mainPage)
    {
        InitializeComponent();
        this.page = mainPage;

        // unnecessary as CreateWindow is doing this for us.
        // MainPage = mainPage;
    }

    // This is only so that I can gain access to MauiContext.ServiceProvider.
    // Is there a cleaner way to access MauiContext outside of this method?
    // I don't like this
    protected override Window CreateWindow(IActivationState activationState)
    {
        windowHelperService = DependencyService.ServiceProvider.GetService<IDesktopEnvironmentService>();

        // passing the MauiContext.ServiceProvider to the window helper service
        // this is needed because I need access to mauiContextServiceProvider.GetService<Microsoft.UI.Xaml.Window>()
        if (windowHelperService != null)
            windowHelperService.UpdateContext(activationState.Context.Services);

        var window = new Window(page);
        return window;
    }

}

