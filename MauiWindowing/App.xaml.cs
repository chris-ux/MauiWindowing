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
        MainPage = mainPage;
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = new Window(page);

        windowHelperService = DependencyService.ServiceProvider.GetService<IDesktopEnvironmentService>();
        if (windowHelperService != null)
            windowHelperService.UpdateContext(activationState.Context.Services);

        return window;
    }

}

